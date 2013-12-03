using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using System.Media;
using ResourceTranslator.Abs;

namespace ResourceTranslator.Impl
{
    public class BingTranslator : Translator
    {

        private Dictionary<string, string> _translations = new Dictionary<string, string>(); 
        public Dictionary<string, string> GetTranslations(LanguageCollection languages, string value)
        {

            AdmAccessToken admToken;
            string headerValue;
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            AdmAuthentication admAuth = new AdmAuthentication("ResourceTranslator2013", "6oiXxi44oAjegBC8LpQIBKnBbFV+hkUa9wSz1K0Sim4=");
            try
            {
                admToken = admAuth.GetAccessToken();
                // Create a header with the access_token property of the returned token
                headerValue = "Bearer " + admToken.access_token;
                TranslateMethod(headerValue, languages, value);
            }
            catch (WebException e)
            {
                ProcessWebException(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

            return _translations;
        }

        private void TranslateMethod(string authToken, LanguageCollection languages, string value)
        {
            string from = "en";
            string translation;
            HttpWebRequest httpWebRequest;
            WebResponse response = null;
            
            try
            {
                
                foreach (var language in languages.Languages)
                {
                    string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" +
                        HttpUtility.UrlEncode(value) + "&from=" + from + "&to=" + language.Key;
                    httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                    httpWebRequest.Headers.Add("Authorization", authToken);
                    response = null;
                    response = httpWebRequest.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    {
                        var dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                        translation = (string)dcs.ReadObject(stream);
                    } 

                    _translations.Add(language.Key, translation);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }

        private static void ProcessWebException(WebException e)
        {
            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.ASCII))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            throw new Exception("Http status code=" + e.Status+ ", error message="+ strResponse);
        }
    }
    [DataContract]
    public class AdmAccessToken
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string expires_in { get; set; }
        [DataMember]
        public string scope { get; set; }
    }

    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private string clientId;
        private string cientSecret;
        private string request;

        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.cientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
        }

        public AdmAccessToken GetAccessToken()
        {
            return HttpPost(DatamarketAccessUri, this.request);
        }

        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                //Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }
    }
}
  
