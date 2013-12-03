using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceTranslator.Abs;

namespace ResourceTranslator.Data
{
    class GbrFilesInfo : FileInfoCollection
    {
        private const string FOLDER_ROOT = @"c:\TranslationTemp\";
        private const string FILE_NAME_START = "gbr.";
        private const string FILE_NAME_EXT = ".resx";

        private List<FileInfo> _filesInfo;
        public GbrFilesInfo(LanguageCollection languageCollection)
        {
            LanguageCollection = languageCollection;
            _filesInfo = new List<FileInfo>();
            
            foreach(var language in languageCollection.Languages)
                _filesInfo.Add(new FileInfo(FOLDER_ROOT +
                    FILE_NAME_START + language.Key + FILE_NAME_EXT));
        }

        public LanguageCollection LanguageCollection { get; set; }
        public IEnumerable<FileInfo> FilesInfo { get { return _filesInfo; }  }

    }
}
