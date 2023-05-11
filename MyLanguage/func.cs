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
    internal class func
    {

        //func string
        public string func_name;
        public string func_value;
        public int lens_code;
        public string result;
        public bool run(Dictionary<string, int> list_int, Dictionary<string, string> list_string, Dictionary<string, double> list_double, Dictionary<string, string> list_func, TextBox otp, string[] element)
        {
            string val1 = "";
            string val2 = "";

            string[] list_func_value = list_func[func_name].Trim().Split(';');
            for (int i = 0; i < list_func_value.Length; ++i) { list_func_value[i] = list_func_value[i].Trim(); }
            string[] func_date = func_value.Replace(" ", "").Split(';');
            for (int i = 0; i < func_date.Length; ++i) { func_date[i] = func_date[i].Trim(); }
            if (list_func_value.Length != func_date.Length)
            {
                otp.Text = "...";
                return false;
            }

            //values
            val1 = func_value.Split(';')[0];
            string nm_vr1 = list_func_value[0].Split(' ')[1];
            string nm_vr2 = "";
            try
            {
                val2 = func_value.Split(';')[1];
                 nm_vr2 = list_func_value[1].Split(' ')[1];
            }
            catch (Exception) { }
            //
   
            #region if variable exists
            if (list_int.ContainsKey(nm_vr1) || list_string.ContainsKey(nm_vr1) || list_double.ContainsKey(nm_vr1))
            {
                otp.Text = $"Variable \'{nm_vr1}\' already exists";
                return false;
            }
            if (list_int.ContainsKey(nm_vr2) || list_string.ContainsKey(nm_vr2) || list_double.ContainsKey(nm_vr2))
            {
                otp.Text = $"Variable \'{nm_vr2}\' already exists";
                return false;
            }
            #endregion

            #region Date variables
            // Variable 1
            if (list_int.ContainsKey(val1))
            {
                val1 = Convert.ToString(list_int[val1]);
            }
            else if (list_double.ContainsKey(val1))
            {
                val1 = Convert.ToString(list_double[val1]);
            }
            else if (list_string.ContainsKey(val1))
            {
                val1 = list_string[val1];
            }
            else { }
            ////

            // Variable 2
            if (list_int.ContainsKey(val2))
            {
                val2= Convert.ToString(list_int[val2]);
            }
            else if (list_double.ContainsKey(val2))
            {
                val2 = Convert.ToString(list_double[val2]);
            }
            else if (list_string.ContainsKey(val2))
            {
                val2 = list_string[val2];
            }
            else { }
            ////
            #endregion

            #region Variable 1
            if (int.TryParse(val1, out int val_int) && list_func_value[0].Split(' ')[0] == "kvint" && !val1.Contains("\""))
            {
                list_int.Add(nm_vr1, Convert.ToInt32(val1));
            }
            else if (val1.Contains(',') && list_func_value[0].Split(' ')[0] == "kvdouble" && !val1.Contains("\""))
            {
                list_double.Add(nm_vr1, Convert.ToDouble(val1));
            }
            else if (list_func_value[0].Split(' ')[0] == "kvstring")
            {
                list_string.Add(nm_vr1, val1);
            }
            else { otp.Text = "Invalid syntax: ..."; }
            #endregion

            if (list_func_value.Length == 1 && func_date.Length == 1)
            { }
            else
            {
                #region Variable 2
                if (int.TryParse(val2, out int val2_int) && list_func_value[1].Split(' ')[0] == "kvint" && !val2.Contains("\""))
                {
                    list_int.Add(nm_vr2, Convert.ToInt32(val2));
                }
                else if (val2.Contains(',') && list_func_value[1].Split(' ')[0] == "kvdouble" && !val2.Contains("\""))
                {
                    list_double.Add(nm_vr2, Convert.ToDouble(val2));
                }
                else if (list_func_value[1].Split(' ')[0] == "kvstring")
                {
                    list_string.Add(nm_vr2, val2);
                }
                else { otp.Text = "Invalid syntax: ..."; }
                #endregion
            }
            while(!element[lens_code].Contains("ret"))
            {
                ++lens_code;
            }
            result = element[lens_code].Split(' ')[1];

            
            return true;
        }
    }
}
