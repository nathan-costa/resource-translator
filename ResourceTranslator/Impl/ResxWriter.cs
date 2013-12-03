using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using ResourceTranslator.Abs;
using ResourceTranslator.Static;

namespace ResourceTranslator.Impl
{
    public class ResxWriter : FileWriter
    {
        private ResXResourceWriter writer;
        private Hashtable resourceEntries;
        public void WriteToFile(FileInfoCollection fileInfoCollection, Dictionary<string, string> translations, string entryKey)
        {
            resourceEntries = new Hashtable(translations);

                foreach (var fileInfo in fileInfoCollection.FilesInfo)
                    foreach (KeyValuePair<string, string> translation in translations)
                        if (fileInfo.Name.ToLower().Contains(translation.Key.ToLower()))
                        {
                            resourceEntries = new Hashtable() {{entryKey, translation.Value}};
                            Helper.UpdateResourceFile(resourceEntries, fileInfo.FullName);
                        }

        }
    }
}
