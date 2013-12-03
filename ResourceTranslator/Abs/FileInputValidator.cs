using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTranslator.Abs
{
    public interface FileInputValidator
    {
        bool KeyExists(FileInfo fileInfo, string key);
    }
}
