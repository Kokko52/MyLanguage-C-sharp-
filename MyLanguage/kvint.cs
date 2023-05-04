using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLanguage
{
    class kvint
    {
        //kvint string
        public string str;
        //elements string kvint
        public string element;
        public string output()
        {
            element = str.Split('(')[1].Split(')')[0].Replace("\"", "");
            return element;
            
        }

    }
}
