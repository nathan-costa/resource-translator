using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ResourceTranslator.Abs;

namespace ResourceTranslator.Impl
{
    class ScrapeTranslator : Translator
    {
        private IWebDriver driver;
        private string baseURL;
        private Dictionary<string, string> _translations = new Dictionary<string, string>(); 

        public Dictionary<string, string> GetTranslations(LanguageCollection languageCollection, string value)
        {
            driver = new FirefoxDriver();
            baseURL = "http://www.nicetranslator.com/";

            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.Id("addTranButn")).Click();
            
            Thread.Sleep(1000);
            foreach(var language in languageCollection.Languages)
                driver.FindElement(By.LinkText(language.Value)).Click();

            driver.FindElement(By.Id("addTranButn")).Click();
            driver.FindElement(By.Id("itxt")).Clear();
            driver.FindElement(By.Id("itxt")).SendKeys(value);

            Thread.Sleep(1000);

            foreach (var language in languageCollection.Languages)
            {
                var parent = driver.FindElement((By.Id("tx" + language.Key)));
                var child = parent.FindElement(By.ClassName("txtxt"));
                _translations.Add(language.Key, child.Text);
            }
            return _translations;
        }
    }
}
