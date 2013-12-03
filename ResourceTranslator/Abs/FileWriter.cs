using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTranslator.Abs
{
    public interface FileWriter
    {
        void WriteToFile(FileInfoCollection fileInfoCollection, Dictionary<string, string> translations, string entryKey);
    }
}
