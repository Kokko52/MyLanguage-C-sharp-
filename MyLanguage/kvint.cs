using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLanguage
{
    //kvint a = 5;
   public class kvint
    {
        //kvint string
        public string str;
        //element string kvint  //volume string kvint
        public string element, volume, string_int;
        public string adding_int()
        {
           // try
           // {
                element = str.Split(' ')[1].Split(' ')[0];
                volume = str.Split('=')[1].Trim();
                string_int = element + " " + volume;
           // }  
            return string_int;        
        }

 
    }
}
