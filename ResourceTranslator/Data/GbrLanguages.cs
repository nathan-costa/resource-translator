using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceTranslator.Abs;

namespace ResourceTranslator.Data
{
    public class GbrLanguages : LanguageCollection
    {

        private Dictionary<string, string> _languages;

        public GbrLanguages()
        {  
            _languages = new Dictionary<string, string>()
            {
                {"it","Italian"         },
                {"ja","Japanese"        },
                {"ko","Korean"          },
                {"nl","Dutch"           },
                {"no","Norwegian"       },
                {"pl","Polish"          },
                {"pt","Portuguese"      },
                {"ru","Russian"         },
                {"sk","Slovak"          },
                {"sl","Slovenian"       },
/*                {"SR","Serbian"         },*/
                {"sv","Swedish"         },
                {"zh-CHS","Chinese | S" },
                {"zh-CHT","Chinese | T" },
                {"da","Danish"          },
                {"de","German"          },
                {"el","Greek"           },
                {"es","Spanish"         },
                {"fi","Finnish"         },
                {"fr","French"          }
            };
        }

        public Dictionary<string, string> Languages
        {
            get { return _languages; }
        }
    }
}
