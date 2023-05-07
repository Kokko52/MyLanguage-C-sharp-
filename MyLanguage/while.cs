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
    class @while
    {
        //string while
        public string str;
        public string value_1, value_2, smbl, elements;

        //symbols
        public static string[] symbols = new string[] { ">=", "<=", ">", "<", "==" };
        public bool run(Dictionary<string, int> list_int, Dictionary<string, string> list_string, Dictionary<string, double> list_double, TextBox otp)
        {
            int check = -1;
            elements = str.Split('(')[1].Split(')')[0];

            //find symbol
            for (int i = 0; i < symbols.Length; ++i)
            {
                if (elements.Contains(symbols[i]))
                {
                    elements = elements.Replace(symbols[i], " ");
                    check = i;
                    break;
                }
            }

            //if symbol no exists
            if (check == -1) { otp.Text = "Invalid syntax: ..."; return false; }

            value_1 = elements.Split(' ')[0];
            value_2 = elements.Split(' ')[1];
            smbl = symbols[check];

            #region Find variable_1
            if (list_int.ContainsKey(value_1))
            {
                value_1 = Convert.ToString(list_int[value_1]);
            }
            else if (list_string.ContainsKey(value_1))
            {
                value_1 = list_string[value_1];
            }
            else if (list_double.ContainsKey(value_1))
            {
                value_1 = Convert.ToString(list_double[value_1]);
            }
            else{}
            #endregion

            #region Find variable_2
            if (list_int.ContainsKey(value_2))
            {
                value_2 = Convert.ToString(list_int[value_2]);
            }
            else if (list_string.ContainsKey(value_2))
            {
                value_2 = list_string[value_2];
            }
            else if (list_double.ContainsKey(value_2))
            {
                value_2 = Convert.ToString(list_double[value_2]);
            }
            else { }
            #endregion

            while (12 > 10 )
            {
                //
            }
            return true;
        }

    }
}
