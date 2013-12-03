using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using ResourceTranslator.Abs;

namespace ResourceTranslator.Static
{
    class Helper
    {
        public static void UpdateResourceFile(Hashtable data, string path, TranslatorForm form)
        {
            form.TextOutput = "Writing " + path + "...";

            Hashtable resourceEntries = new Hashtable();
            bool check = false;

            //Get existing resources
            ResXResourceReader reader = new ResXResourceReader(path);
            if (reader != null)
            {
                IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Value == null)
                        resourceEntries.Add(d.Key.ToString(), "");
                    else
                        resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                }
                reader.Close();
            }

            //Modify resources here...
            foreach (String key in data.Keys)
            {
                if (!resourceEntries.ContainsKey(key))
                {

                    String value = data[key].ToString();
                    if (value == null) value = "";

                    resourceEntries.Add(key, value);
                }
            }

            //Write the combined resource file
            ResXResourceWriter resourceWriter = new ResXResourceWriter(path);

            foreach (String key in resourceEntries.Keys)
            {
                resourceWriter.AddResource(key, resourceEntries[key]);
            }
            resourceWriter.Generate();
            resourceWriter.Close();

            //Check if entered correctly
            reader = new ResXResourceReader(path);
            if (reader != null)
            {
                foreach (DictionaryEntry d in reader)
                    foreach (String key in resourceEntries.Keys)
                    {
                        if ((string) d.Key == key && (string) d.Value == (string) resourceEntries[key]) check = true;
                    }
                reader.Close();
            }

            if (check) form.TextOutput = path + " written successfully";
            else form.TextOutput = path + " not written !!";

        }
    }
}
