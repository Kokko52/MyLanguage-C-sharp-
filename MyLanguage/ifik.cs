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
        public static string[] symbol = new string[] { ">=", "<=", ">", "<", "==" };
        public int ch = -1;
        public int lens_code = 0;
        public int LENS = 0;

        public bool run(Dictionary<string, int> list_int, Dictionary<string, string> list_string, Dictionary<string, double> list_double, TextBox otp, string[] element)
        {
            // ifik(...)
            elements = str.Split('(')[1].Split(')')[0];
            //split elements
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

            #region elements
            //variable 1
            el_1 = elements.Split(' ')[0];
            //symbol
            el_2 = symbol[ch];
            //variable 2
            el_3 = elements.Split(' ')[1];
            #endregion

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
            switch (el_2)
            {
                case ">":
                    {
                        //if true
                        if (Convert.ToDouble(el_1) > Convert.ToDouble(el_3))
                         {
                          //  ++lens_code;
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
                case "<":
                    {
                        //if true
                        if (Convert.ToDouble(el_1) < Convert.ToDouble(el_3))
                        {
                           // ++lens_code;
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
                case "==":
                    {
                        //if true
                        if (Convert.ToDouble(el_1) == Convert.ToDouble(el_3))
                        {
                            //++lens_code;
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
                case ">=":
                    {
                        //if true
                        if (Convert.ToDouble(el_1) >= Convert.ToDouble(el_3))
                        {
                           // ++lens_code;
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
                case "<=":
                    {
                        //if true
                        if (Convert.ToDouble(el_1) <= Convert.ToDouble(el_3))
                        {
                           // ++lens_code;
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
            return true;
        }      
    }
}
