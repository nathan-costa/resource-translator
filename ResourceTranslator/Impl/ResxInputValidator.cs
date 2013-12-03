using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ResourceTranslator.Abs;

namespace ResourceTranslator.Impl
{
    class ResxInputValidator : FileInputValidator
    {
        private ResXResourceReader reader;
        public bool KeyExists(FileInfo fileInfo, string key)
        {
            using (reader)
            {
                reader = new ResXResourceReader(fileInfo.FullName);
                var enumerator = reader.GetEnumerator();

                foreach (DictionaryEntry d in reader)
                    if (String.Equals(d.Key.ToString(), key, StringComparison.CurrentCultureIgnoreCase)) return true;

            }
            return false;
        }
        
    }
}
