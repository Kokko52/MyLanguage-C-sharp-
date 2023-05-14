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
    class kvprint
    {
        //kv.print string
        public string str;
        //elements string kv.print
        public string name;
        public bool run(Dictionary<string, int> list_int, Dictionary<string, string> list_string, Dictionary<string, double> list_double, TextBox otp)
        {
            try
            {
                //geting the value
                name = str.Split('(')[1].Split(')')[0];
            }
            catch (IndexOutOfRangeException) { otp.Text = $"Syntax invalid: {str}   -   format exeption"; return false; }
            try
            {
                int ch_ = 0;
                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] == '"')
                    {
                        ++ch_;
                    }
                }
                if (ch_ == 1) { otp.Text = $"Syntax invalid: {str}  -   not exist bracket"; return false; }
                else if (ch_ > 2) { otp.Text = $"Syntax invalid: {str}  -   lots of brackets"; return false; }
                name = name.Split('"')[1].Split('\"')[0];
                otp.Text += "\n";
                otp.Text += name;
                otp.Text += "\r";
            }
            catch (IndexOutOfRangeException)
            {
                if (list_int.ContainsKey(name))
                {
                    otp.Text += "\n";
                    otp.Text += Convert.ToString(list_int[name]);
                    otp.Text += "\r";
                }
                else if (list_string.ContainsKey(name))
                {
                    otp.Text += "\n";
                    otp.Text += list_string[name];
                    otp.Text += "\r";
                }
                else if (list_double.ContainsKey(name))
                {
                    otp.Text += "\n";
                    otp.Text += Convert.ToString(list_double[name]);
                    otp.Text += "\r";
                }
                else
                {
                    if (int.TryParse(name, out int val_int))
                    {
                        otp.Text += "\n";
                        otp.Text += name;
                        otp.Text += "\r";
                    }
                    else
                    {

                        otp.Text = $"Syntax invaldi: {str}  -   format exeption";
                        return false;
                    }
                    ;                    //otp.Text += "\n";
                    //otp.Text += name;
                    //otp.Text += "\r";
                }
            }
            return true;
        }
    }
}
