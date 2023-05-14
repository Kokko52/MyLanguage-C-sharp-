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
    class ifik
    {
        //ifik string  
        public string str;
        //elements string
        public string el_1, el_2, el_3, elements;
        //symbols
        public static string[] symbol = new string[] { ">=", "<=", ">", "<", "==" };
        //check errors not exists symbol
        public int ch = -1;

        //lines code
        public int lens_code = 0;
        //check exists 'ifik'
        public int if_true = 0;


        public bool run(Dictionary<string, int> list_int, Dictionary<string, string> list_string, Dictionary<string, double> list_double, TextBox otp, string[] element)
        {
            // ifik(...)
            try
            {
                elements = str.Split('(')[1].Split(')')[0];
            }
            catch (IndexOutOfRangeException) { otp.Text = $"Invalid syntax: {str}   -   no exists bracket";return false; }
            //split elements (...)
            for (int i = 0; i < symbol.Length; i++)
            {
                //if exixts symbol
                if (elements.Contains(symbol[i]))
                {
                    ch = i;
                    elements = elements.Replace(symbol[ch], " ");
                }
            }
            //if not exists symbol
            if (ch == -1)
            {
                otp.Text = $"Invalid sytaxis in line - {str}";
                return false;
            }

            #region getting names for variables
            //variable 1
            el_1 = elements.Split(' ')[0];
            //symbol
            el_2 = symbol[ch];
            //variable 2
            el_3 = elements.Split(' ')[1];
            #endregion

            #region getting values for variables
            if (list_int.ContainsKey(el_1))
            {
                el_1 = Convert.ToString(list_int[el_1]);
            }
            else if (list_string.ContainsKey(el_1))
            {
                el_1 = list_string[el_1];
            }
            else if (list_double.ContainsKey(el_1))
            {
                el_1 = Convert.ToString(list_double[el_1]);
            }
            else { }

            if (list_int.ContainsKey(el_3))
            {
                el_3 = Convert.ToString(list_int[el_3]);
            }
            else if (list_string.ContainsKey(el_3))
            {
                el_3 = list_string[el_3];
            }
            else if (list_double.ContainsKey(el_3))
            {
                el_3 = Convert.ToString(list_double[el_3]);
            }
            else { }
            #endregion

            try
            {
                //finding symbol
                switch (el_2)
                {
                    case ">":
                        {
                            //if true
                            if (Convert.ToDouble(el_1) > Convert.ToDouble(el_3))
                            {
                                //wenting 'ifik' 
                                if_true = 1;                             
                                return true;
                            }
                            //skip ifik
                            else
                            {
                                //finding end ifik()
                                while (!element[lens_code].Contains('}'))
                                {
                                    lens_code++;
                                }
                                    return true;
                            }
                        }
                    case "<":
                        {
                            //if true
                            if (Convert.ToDouble(el_1) < Convert.ToDouble(el_3))
                            {
                                //wenting 'ifik' 
                                if_true = 1;
                                return true;
                            }
                            //skip ifik
                            else
                            {
                                //finding end ifik()
                                while (!element[lens_code].Contains('}'))
                                {
                                    lens_code++;
                                }
                                return true;
                            }
                        }
                    case "==":
                        {
                            //if true
                            if (Convert.ToDouble(el_1) == Convert.ToDouble(el_3))
                            {
                                //wenting 'ifik' 
                                if_true = 1;
                                return true;
                            }
                            //skip ifik
                            else
                            {
                                //finding end ifik()
                                while (!element[lens_code].Contains('}'))
                                {
                                    lens_code++;
                                }
                                return true;
                            }
                        }
                    case ">=":
                        {
                            //if true
                            if (Convert.ToDouble(el_1) >= Convert.ToDouble(el_3))
                            {
                                //wenting 'ifik' 
                                if_true = 1;
                                return true;
                            }
                            //skip ifik
                            else
                            {
                                //finding end ifik()
                                while (!element[lens_code].Contains('}'))
                                {
                                    lens_code++;
                                }
                                return true;
                            }
                        }
                    case "<=":
                        {
                            //if true
                            if (Convert.ToDouble(el_1) <= Convert.ToDouble(el_3))
                            {
                                //wenting 'ifik' 
                                if_true = 1;
                                return true;
                            }
                            //skip ifik
                            else
                            {
                                //finding end ifik()
                                while (!element[lens_code].Contains('}'))
                                {
                                    lens_code++;
                                }
                                return true;
                            }
                            break;
                        }
                }
            }
            catch (FormatException) { otp.Text = $"Invalid data: {str}"; return false; }
            return true;
        }
    }
}
