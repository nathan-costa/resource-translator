/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using NUnit.Core;
using NUnit.Framework;
using ResourceTranslator.Data;
using ResourceTranslator.Static;

namespace ResourceTranslator.Tests
{
    [TestFixture]
    public class HelperTests
    {
        [Test]
        public void WriteToFileWritesToAllFiles()
        {
            bool check = false;
            ResXResourceReader reader;
            var languages = new GbrLanguages();
            var filesInfo = new GbrFilesInfo(languages);
            string key, value;
            Hashtable map;
            foreach (var fileInfo in filesInfo.FilesInfo)
            {
                check = false;
                key = Guid.NewGuid().ToString();
                value = fileInfo.Name;
                map = new Hashtable() {{key, value}};
                Helper.UpdateResourceFile(map, fileInfo.FullName);
                reader = new ResXResourceReader(fileInfo.FullName);
                if (reader != null)
                {
                    IDictionaryEnumerator id = reader.GetEnumerator();
                    foreach (DictionaryEntry d in reader)
                    {
                        if ((string) d.Key == key)
                        {
                            check = true;
                            Assert.AreEqual(d.Key, key);
                            Assert.AreEqual(d.Value, value);
                        }
                    }

                    if (!check) Assert.Fail("Key not found! " + fileInfo.Name);
                    reader.Close();
                }

                
            }
        }
    }
}
*/
