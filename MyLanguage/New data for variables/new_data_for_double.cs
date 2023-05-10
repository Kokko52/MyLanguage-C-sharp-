using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLanguage
{
    internal class new_data_for_double
    {
        public bool run(string[] element, int lens_code, Dictionary<string, double> list_double, Dictionary<string, int> list_int, string variable)
        {
            string[] symbols = new string[] { "+", "-", "*", "/" };

            //orig string
            string line = element[lens_code].Split('=')[1].Replace(" ", "");
            //split string
            string[] line_split = new string[line.Length];
            //check
            int cnt = 0;
            for (int i = 0; i < line.Length; ++i)
            {
                //add elements
                while (line[cnt] != '+' && line[cnt] != '-' && line[cnt] != '*' && line[cnt] != '/' && cnt < line.Length)
                {
                    line_split[i] += line[cnt];
                    ++cnt;
                    if (cnt == line.Length) { break; }
                }
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
                if (list_double.ContainsKey(line_split[cnt]))
                {
                    line_split[cnt] = Convert.ToString(list_double[line_split[cnt]]);
                }
                if(list_int.ContainsKey(line_split[cnt]))
                {
                    line_split[cnt] = Convert.ToString(list_int[line_split[cnt]]);
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
                    ArrayList line_split_list = new ArrayList(line_split);
                    line_split_list[cnt + 1] = Convert.ToString(Convert.ToDouble(line_split[cnt + 1]) * Convert.ToDouble(line_split[cnt - 1]));
                    line_split_list.RemoveAt(cnt);
                    line_split_list.RemoveAt(cnt - 1);
                    line_split = (string[])line_split_list.ToArray(typeof(string));
                    cnt = 0;
                    //
                }
                else if (line_split[cnt] == "/")
                {
                    ArrayList line_split_list = new ArrayList(line_split);
                    line_split_list[cnt + 1] = Convert.ToString(Convert.ToDouble(line_split[cnt - 1]) / Convert.ToDouble(line_split[cnt + 1]));
                    line_split_list.RemoveAt(cnt);
                    line_split_list.RemoveAt(cnt - 1);
                    line_split = (string[])line_split_list.ToArray(typeof(string));
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
                    line_split_list[cnt + 1] = Convert.ToString(Convert.ToDouble(line_split[cnt + 1]) + Convert.ToDouble(line_split[cnt - 1]));
                    line_split_list.RemoveAt(cnt);
                    line_split_list.RemoveAt(cnt - 1);
                    line_split = (string[])line_split_list.ToArray(typeof(string));
                    cnt = 0;
                }
                else if (line_split[cnt] == "-")
                {
                    ArrayList line_split_list = new ArrayList(line_split);
                    line_split_list[cnt + 1] = Convert.ToString(Convert.ToDouble(line_split[cnt - 1]) - Convert.ToDouble(line_split[cnt + 1]));
                    line_split_list.RemoveAt(cnt);
                    line_split_list.RemoveAt(cnt - 1);
                    line_split = (string[])line_split_list.ToArray(typeof(string));
                    cnt = 0;
                }
                ++cnt;
            }
            //new value
            list_double[variable] = Convert.ToDouble(line_split[0]);

            return true;
        }
    }
}
