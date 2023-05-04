using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLanguage
{
    class kvprint
    {
        //kvint string
        public string str;
        //elements string kvint
        public string element;
        public string output()
        {
            //geting the value
            element = str.Split('(')[1].Split(')')[0].Replace("\"", "");
            return element;         
        }

    }
}
