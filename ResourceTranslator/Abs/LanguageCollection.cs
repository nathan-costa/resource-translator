using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTranslator.Abs
{
    public interface LanguageCollection
    {
        Dictionary<string, string> Languages { get; }
    }
}
