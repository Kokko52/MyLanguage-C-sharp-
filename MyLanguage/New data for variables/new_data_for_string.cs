using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLanguage
{
    class new_data_for_string
    {
        public int lens_code;
        public bool run(string[] element, Dictionary<string, string> list_string, string variable, TextBox otp)
        {
            string[] symbols = new string[] { "+", "-", "*", "/" };
            string line = "";

            //orig string 
            try
            {
                line = element[lens_code].Split('=')[1];//.Replace(" ", "");
            }
            catch (IndexOutOfRangeException) { otp.Text = $"Syntax invalid: {element[lens_code]}    -   \'=\' ?";return false; }
            //split string
            string[] line_split = new string[line.Length + 1];
            //check
            int cnt = 0;
            for (int i = 0; i < line.Length + 1; ++i)
            {
                try
                {
                    //add elements
                    while (line[cnt] != '+' && line[cnt] != '-' && line[cnt] != '*' && line[cnt] != '/' && cnt < line.Length)
                    {
                        line_split[i] += line[cnt];
                        ++cnt;
                        if (cnt == line.Length) { break; }
                    }
                }
                catch (Exception) { otp.Text = $"Invalid syntax: {element[lens_code]}   -   format exeption"; return false; }
                ++i;
                //exit
                if (cnt == line.Length) { break; }
                line_split[i] += line[cnt];
                ++cnt;
                //exit
                if (cnt == line.Length) { break; }
            }

            //clear check
            cnt = 0;

            //find variables
            while (cnt < line_split.Length && line_split[cnt] != null)
            {
                if (list_string.ContainsKey(line_split[cnt].Trim()))
                {
                    line_split[cnt] = Convert.ToString(list_string[line_split[cnt].Trim()]);
                }
                ++cnt;
            }
            //clear check
            cnt = 0;
            //find '*' and '/'
            while (cnt < line_split.Length)
            {
                if (line_split[cnt] == "*")
                {
                    otp.Text = $"Invalid syntax: \'{element[lens_code]}\'   -  invalid symbol ";
                    cnt = 0;
                    return false;
                    cnt = 0;
                }
                else if (line_split[cnt] == "/")
                {
                    otp.Text = $"Invalid syntax: \'{element[lens_code]}\'   -  invalid symbol ";
                    cnt = 0;
                    return false;
                    cnt = 0;
                }
                ++cnt;
            }

            //clear check
            cnt = 0;

            //find '+' and '-'
            while (cnt < line_split.Length)
            {
                if (line_split[cnt] == "+")
                {
                    ArrayList line_split_list = new ArrayList(line_split);
                    line_split_list[cnt + 1] = Convert.ToString(line_split[cnt + 1] + line_split[cnt - 1]);
                    line_split_list.RemoveAt(cnt);
                    line_split_list.RemoveAt(cnt - 1);
                    line_split = (string[])line_split_list.ToArray(typeof(string));
                    cnt = 0;
                }
                else if (line_split[cnt] == "-")
                {
                    otp.Text = $"Invalid syntax: \'{element[lens_code]}\'   -  invalid symbol ";
                    cnt = 0;
                    return false;
                }
                ++cnt;
            }
            //new value
            try
            {
            list_string[variable] = new string(line_split[0].Reverse().ToArray());
            }
            catch (FormatException) { otp.Text = "Syntax invalid: format exeption"; return false; }
            return true;
        }
    }
}
