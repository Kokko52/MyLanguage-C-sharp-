using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLanguage
{
    class kvprint
    {
        //kv.print string
        public string str;
        //elements string kv.print
        public string element;
        public string output()
        {
            //geting the value
            element = str.Split('(')[1].Split(')')[0].Replace("\"", "");
            
            for(int i = 0; i < 4; ++i)
            {

            }
            return element;         
        }

    }
}
