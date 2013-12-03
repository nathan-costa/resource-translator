using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ResourceTranslator.Abs;

namespace ResourceTranslator.Impl
{
    public class ResourceTranslator
    {
        private FileInfoCollection _fileInfoCollection;
        private FileInputValidator _fileInputValidator;
        private List<string> _languges;
        private string _key;

        public ResourceTranslator(FileInfoCollection fileInfoCollection, FileInputValidator fileInputValidator)
        {
            _fileInfoCollection = fileInfoCollection;
            _fileInputValidator = fileInputValidator;
        }

        public void ValidateResources(string key)
        {
            _key = key;
            foreach (FileInfo fileInfo in _fileInfoCollection.FilesInfo)
            {
                if (_fileInputValidator.KeyExists(fileInfo, key)) throw new Exception("Key exists in " + fileInfo.Name);
            }
        }

        public Dictionary<string, string> Translate(Translator translator, string text)
        {
           return translator.GetTranslations(_fileInfoCollection.LanguageCollection, text);
        }


        public void WriteToFile(FileWriter writer, Dictionary<string, string> translations)
        {
            writer.WriteToFile(_fileInfoCollection, translations, _key);
        }
    }
}
