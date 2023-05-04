using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Linq;
using System.Windows.Forms;

namespace MyLanguage
{
    public class kvint
    {
        //kvint string
        public string str;
        //element string kvint
        public string name;
        //volume string kvint
        public int volume;

        public bool run(Dictionary<string, int> Int, Dictionary<string, string> Str, Dictionary<string, double> Doubl, TextBox otp)
        {
            //seting name and volume
            name = str.Split(' ')[1].Split(' ')[0];
            volume = Convert.ToInt32(str.Split('=')[1].Trim());
            //

            //if variable exists
            if (Int.ContainsKey(name) || Str.ContainsKey(name) || Doubl.ContainsKey(name))
            {
                otp.Text = $"Variable \'{name}\' with the volume \'{volume}\' already exists";
                return false;
            }
            //else add variable
            else
            {
                //add variable
                Int.Add(name, volume);
            }

            return true;
        }
    }
}
