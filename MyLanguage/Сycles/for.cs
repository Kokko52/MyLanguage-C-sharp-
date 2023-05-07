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
    class @for
    {
        //for string
        public string str;
        //split ';'(count - 3)
        public string[] elements;
        public int lens_code;
        public string name_variable, name_variables2;
        public int variable_volume;
        public int number_limiter = 0;
        public string symbol_simile;

        //symbols
        public static string[] symbol = new string[] { ">=", "<=", ">", "<", "==" };
        //check symbol
        public int symbol_check = -1;
        public string symbol_cont;
        public bool run(TextBox otp, string[] element)
        {
            //for (kvint i=0;i<5;++i)
            #region Geting dates
            elements = str.Split(';');
            if (elements.Length < 3) { otp.Text = "Invalid syntax: no exists ';'"; return false; }
            else if (elements.Length > 3) { otp.Text = "Invalid syntax: excess ';'"; return false; }

            name_variable = elements[0].Split(' ')[2].Split('=')[0];
            variable_volume = Convert.ToInt32(elements[0].Split('=')[1]);

            //find symbol
            for(int i = 0; i < symbol.Length; i++)
            {
                if (elements[1].Contains(symbol[i])) 
                { 
                    elements[1] = elements[1].Replace(symbol[i], " ");
                    symbol_check = i; 
                    break;
                }
            }
            //if no exists symbol
            if (symbol_check == -1) { otp.Text = $"Invalid syntax: missing comparison sign in string - {str}";return false; }

            name_variables2 = elements[1].Split(' ')[0];
            symbol_simile = symbol[symbol_check];
            number_limiter = Convert.ToInt32(elements[1].Split(' ')[1]);

            symbol_cont = Convert.ToString(elements[2][0]) + Convert.ToString(elements[2][1]);
            if (symbol_cont != "++" && symbol_cont != "--") { otp.Text = $"Invalid syntax: invalid operation symbol - {elements[2]} "; return false; }
            #endregion

            switch (symbol_simile)
            {
                case ">":
                    {
                        break;
                    }
                case "<":
                    {
                        if(symbol_cont == "++")
                        {
                            for (int i = variable_volume; i < number_limiter; ++i)
                            {

                            }
                        }
                        else
                        {

                        }
                            break;
                    }
            }

            
            return true;
        }
    }
}
