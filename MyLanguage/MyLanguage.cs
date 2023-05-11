using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MyLanguage
{
    public partial class MyLanguage : Form
    {
        #region textBoxs
        //textBox1 - code
        public TextBox code = new TextBox();
        //textBox2 - otp
        public TextBox otp = new TextBox();
        #endregion

        #region Storages Variables
        //list int
        public Dictionary<string, int> list_int = new Dictionary<string, int>();
        //list string
        public Dictionary<string, string> list_string = new Dictionary<string, string>();
        //list double
        public Dictionary<string, double> list_double = new Dictionary<string, double>();
        #endregion

        //list functions
        public Dictionary<string, string> list_func = new Dictionary<string, string>(); // name + variabes
        public Dictionary<string, string> list_func_result = new Dictionary<string, string>(); // name + result
        public Dictionary<string, int> list_func_str = new Dictionary<string, int>(); // name + №string
        //original string
        public string str;
        //lines
        public string[] element = new string[] { };
        //lens code 
        public int lens_code = 0;
        //check exists ifik
        public int _if = 0;
        //checking brackets
        public int _sk = 0;
        public int check_for, check_wh = 0;
        public int end_wh = 0;
        public int func_start, last_lens_code;
        //check func
        public bool func_check = false;
        //name func
        public string name_func;
        //name functions and its values
        public string func_name_and_var;
        //result func
        public string result;
        public MyLanguage()
        {
            InitializeComponent();

            #region TextBoxs
            //TextBox1 - code
            code.Location = new Point(12, 42);
            code.Size = new Size(845, 383);
            code.Multiline = true;
            code.ScrollBars = ScrollBars.Both;
            code.Text = "KV{}";
            code.Font = new Font("Times New Roman", 16);
            Controls.Add(code);
            //

            //TextBox2 - output
            otp.Location = new Point(12, 487);
            otp.Size = new Size(845, 118);
            otp.Multiline = true;
            otp.ScrollBars = ScrollBars.Both;
            otp.Font = new Font("Times New Roman", 16);
            otp.ReadOnly = true;
            Controls.Add(otp);
            #endregion
        }
        public void Run()
        {
            //clearing
            str = code.Text.Replace("\n", "").Trim();

            //seting elements string
            element = str.Split('\r');

            //removing spaces
            for (int i = 0; i < element.Length; i++)
            {
                element[i] = element[i].Trim();
            }

            #region main func - 'KV'
            ////main func 'KV{'
            //if (element[0] != "KV")
            //{
            //    otp.Text = $"Syntax error: main function not found - {element[0]}";
            //    return;
            //}
            ////end main func - KV
            //if (element[element.Length - 1] != "}")
            //{
            //    otp.Text = "Syntax error: end main function not found";
            //    return;
            //}
            #endregion
            int lens_code = 0;
            //func
            while (element[lens_code] != "KV{")
            {
                if (element[lens_code].Split(' ')[0] == "func")
                {

                    func_start = lens_code + 1;

                    string name_func = element[lens_code].Split(' ')[1].Split('(')[0].Trim();
                    string opt = element[lens_code].Split('(')[1].Split(')')[0];
                    if (list_func.ContainsKey(name_func)) { otp.Text = $"Invalid syntax: a function {name_func} with this name already exists"; return; }
                    list_func.Add(name_func, opt/* Convert.ToString(func_start)*/);
                    list_func_str.Add(name_func, func_start);
                }
                lens_code++;
            }
            while (element[lens_code] != "KV{")
            {
                lens_code++;
            }

            //lens_code = 1;
            while (lens_code < element.Length)
            {
            m_return:

            #region checking brackets
            ////skip null line
            //if (element[lens_code] == "") { }
            ////exists brackets '{'
            //else if (element[lens_code] == "{" || element[lens_code][element[lens_code].Length - 1] == '{') { _sk++; }
            ////exists brackets '}'
            //else if (element[lens_code] == "}" || element[lens_code] == "};" || element[lens_code][element[lens_code].Length - 1] == '}') { _sk--; }
            #endregion

            #region math library
            m_math_lib:
                //Math.Round
                if (element[lens_code].Contains("Math.Round"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Round{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Round(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Round{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Floor
                if (element[lens_code].Contains("Math.Floor"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Floor{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Floor(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Floor{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Ceiling
                if (element[lens_code].Contains("Math.Ceiling"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Ceiling{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Ceiling(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Ceiling{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Sign
                if (element[lens_code].Contains("Math.Sign"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Sign{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Sign(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Sign{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Pow
                if (element[lens_code].Contains("Math.Pow"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Pow{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }                    //date

                    string new_var = mth.Split('{')[1];
                    string[] variables = new_var.Split(';');
                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Pow(Convert.ToDouble(variables[0]), Convert.ToDouble(variables[1]));
                    value = Math.Round(value, 2);
                    //error!!!!!!!!!!!!!!!!!!!!!!         
                    element[lens_code] = element[lens_code].Replace("Math.Pow{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Cos
                if (element[lens_code].Contains("Math.Cos"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Cos{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Cos(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Cos{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Sinh
                if (element[lens_code].Contains("Math.Sinh"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Sinh{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Sinh(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Sinh{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Sin
                if (element[lens_code].Contains("Math.Sin"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Sin{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Sin(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Sin{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                //Math.Sqrt
                if (element[lens_code].Contains("Math.Sqrt"))
                {
                    string str = element[lens_code];
                    int last = element[lens_code].LastIndexOf("Math.Sqrt{");
                    string mth = "";
                    while (element[lens_code][last] != '}')
                    {
                        mth += element[lens_code][last];
                        ++last;
                    }
                    //date
                    string new_var = mth.Split('{')[1];

                    #region Date variables
                    if (list_int.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_int[new_var]);
                    }
                    else if (list_double.ContainsKey(new_var))
                    {
                        new_var = Convert.ToString(list_double[new_var]);
                    }
                    else if (list_string.ContainsKey(new_var))
                    {
                        new_var = list_string[new_var];
                    }
                    else { }
                    #endregion

                    double value = Math.Sqrt(Convert.ToDouble(new_var));
                    element[lens_code] = element[lens_code].Replace("Math.Sqrt{" + new_var + "}", Convert.ToString(value));
                    goto m_math_lib;
                }
                #endregion

             #region str library
                #endregion

                #region func
                //name functions
                for (int i = 0; i < list_func.Count; i++)
                {
                    //if exist func
                    if (element[lens_code].Contains(list_func.ElementAt(i).Key))
                    {
                        last_lens_code = lens_code;
                        func_check = true;
                        //values
                        string func_date = element[lens_code].Replace(list_func.ElementAt(i).Key, "~").Split('~')[1].Replace("(", "").Replace(")", "");
                        func func = new func();
                        //name function
                        func.func_name = list_func.ElementAt(i).Key;
                        //values function
                        func.func_value = func_date;
                        func.lens_code = list_func_str[func.func_name];
                        func.run(list_int, list_string, list_double, list_func, otp, element);
                        //lens_code = func_start;
                        func_name_and_var = func.func_name + "(" + func.func_value + ")";
                        result = func.result;
                        name_func = func.func_name;
                        element[lens_code] = element[lens_code].Replace(func_name_and_var, func.result);
                        lens_code = list_func_str[func.func_name];
                    }
                }
                //check
                if (func_check)
                {
                    element[lens_code].Replace(func_name_and_var, result);
                    func_check = false;
                }
                //replace
                if (element[lens_code].Contains("func " + name_func))
                {
                    while (!element[lens_code].Contains("ret"))
                    {
                        lens_code++;
                    }
                    list_func_result.Add(name_func, element[lens_code].Split(' ')[1]);
                    lens_code = last_lens_code;
                }
                //if end functions
                if (element[lens_code].Split(' ')[0] == "ret")
                {
                    string func_res = element[lens_code].Split(' ')[1];
                    if (list_int.ContainsKey(func_res))
                    {
                        func_res = Convert.ToString(list_int[func_res]);
                    }
                    else if (list_string.ContainsKey(func_res))
                    {
                        func_res = list_string[func_res];
                    }
                    else if (list_double.ContainsKey(func_res))
                    {
                        func_res = Convert.ToString(list_double[func_res]);
                    }
                    else
                    { }
                    lens_code = last_lens_code;
                    goto m_return;
                    //  list_func_result.Add();
                }

                //kv.print
                if (element[lens_code].Split(' ')[0] == "kv.print")
                {
                    kvprint kvprint = new kvprint();
                    kvprint.str = element[lens_code].Trim();
                    if (!kvprint.run(list_int, list_string, list_double, otp)) { break; }
                }
                #endregion

                #region Data types
                //kvint
                if (element[lens_code].Split(' ')[0] == "kvint")
                {
                    kvint kvint = new kvint();
                    kvint.str = element[lens_code].Trim();
                    if (!kvint.run(list_int, list_string, list_double, otp)) { break; }
                }
                //kvstring
                if (element[lens_code].Split(' ')[0] == "kvstring")
                {
                    kvstring kvstring = new kvstring();
                    kvstring.str = element[lens_code].Trim();
                    if (!kvstring.run(list_int, list_string, list_double, otp)) { break; }
                }
                //kvdouble
                if (element[lens_code].Split(' ')[0] == "kvdouble")
                {
                    kvdouble kvdouble = new kvdouble();
                    kvdouble.str = element[lens_code].Trim();
                    if (!kvdouble.run(list_int, list_string, list_double, otp)) { break; };
                }
                #endregion

                #region if-else
                //ifik
                if (element[lens_code].Split(' ')[0] == "ifik")
                {
                    ifik ifik = new ifik();
                    //pass lines code
                    ifik.lens_code = lens_code;
                    //pass line 'ifik(...)'
                    ifik.str = element[lens_code].Trim().Replace(" ", "");
                    if (!ifik.run(list_int, list_string, list_double, otp, element)) { break; }
                    //new lines_code
                    lens_code = ifik.lens_code;
                    //new if_exists
                    _if = ifik.if_true;
                }
                //elsik
                if (element[lens_code].Split(' ')[0] == "elsik")
                {
                    //if not find '}'
                    if (element[lens_code - 1] != "}" && element[lens_code - 1][element[lens_code - 1].Length - 1] != '}')
                    {
                        //error
                        otp.Text = "Invalid syntaxis: not exists \'ifik\'";
                    }
                    //if went to 'ifik' 
                    if (_if == 1)
                    {
                        //skip
                        while (!element[lens_code].Contains('}'))
                        {
                            ++lens_code;
                        }
                        _sk--;
                    }
                    //clear
                    _if = 0;
                }
                #endregion

                #region cycle           
                //while
                if (element[lens_code].Split(' ')[0] == "while")
                {
                    //find end while
                    for (int i = lens_code; i < element.Length; i++)
                    {
                        if (element[i] == "};")
                        {
                            end_wh = i;
                            break;
                        }
                    }

                    @while _while = new @while();
                    _while.str = element[lens_code].Replace(" ", "");
                    if (_while.run(list_int, list_string, list_double, otp)) { check_wh = lens_code; }
                    else { lens_code = end_wh + 1; goto m_return; }
                }
                //end while
                if (element[lens_code].Trim() == "};")
                {
                    if (check_wh == 0)
                    {
                        otp.Text = "Invalid syntax: ...";
                        break;
                    }
                    else if (check_wh == -1)
                    { }
                    else
                    {
                        lens_code = check_wh - 1;
                    }
                }
                #endregion

                #region New data for variable
                //variable - int
                if (list_int.ContainsKey(element[lens_code].Split(' ')[0]))
                {
                    new_data_for_int cls = new new_data_for_int();
                    cls.lens_code = lens_code;
                    cls.run(element, list_int, element[lens_code].Split(' ')[0]);
                    lens_code = cls.lens_code;

                }
                //variable - double
                else if (list_double.ContainsKey(element[lens_code].Split(' ')[0]))
                {
                    new_data_for_double cls = new new_data_for_double();
                    cls.run(element, lens_code, list_double, list_int, element[lens_code].Split(' ')[0]);
                }
                #endregion


                ++lens_code;
            }
            #region Error: checking brackets
            //if (_sk > 1) { otp.Text = "Invalid syntax: missing closing brackets"; }
            //else if (_sk > 0) { otp.Text = "Invalid syntax: missing closing bracket"; }
            //else if (_sk == -1) { otp.Text = "Invalid syntax: missing opening bracket"; }
            //else if (_sk < 0) { otp.Text = "Invalid syntax: missing opening brackets"; }
            #endregion
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
            code.BackColor = Color.FromArgb(30, 30, 30);
            otp.BackColor = Color.FromArgb(30, 30, 30);
            // label1.BackColor = Color.FromArgb(53, 53, 53);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear
            otp.Text = "";
            list_int.Clear();
            list_string.Clear();
            list_double.Clear();
            list_func.Clear();
            list_func_result.Clear();
            list_func_str.Clear();
            lens_code = 0;
            _sk = 0;
            //
            Run();
        }
    }
}
