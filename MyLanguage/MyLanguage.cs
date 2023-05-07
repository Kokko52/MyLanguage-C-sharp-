﻿using System;
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
            code.Font = new Font("Times New Roman", 14);
            Controls.Add(code);
            //

            //TextBox2 - output
            otp.Location = new Point(12, 487);
            otp.Size = new Size(845, 118);
            otp.Multiline = true;
            otp.ScrollBars = ScrollBars.Both;
            otp.Font = new Font("Times New Roman", 14);
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

            for (int i = 0; i < element.Length; i++)
            {
                if (element[i] == "};")
                {
                    end_wh = i;
                    break;
                }
            }
            #region main func - 'KV'
            //main func 'KV{'
            if (element[0] != "KV")
            {
                otp.Text = $"Syntax error: main function not found - {element[0]}";
                return;
            }
            //end main func - KV
            if (element[element.Length - 1] != "}")
            {
                otp.Text = "Syntax error: end main function not found";
                return;
            }
            #endregion
            int lens_code = 1;
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

                //kv.print
                if (element[lens_code].Split(' ')[0] == "kv.print")
                {
                    kvprint kvprint = new kvprint();
                    kvprint.str = element[lens_code].Trim();
                    if (!kvprint.run(list_int, list_string, list_double, otp)) { break; }
                }

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
                //for
                if (element[lens_code].Split(' ')[0] == "for")
                {
                    check_for = lens_code;
                    @for _for = new @for();
                    _for.str = element[lens_code].Trim();
                    _for.lens_code = lens_code;

                    _for.run(otp, element);
                }
                //end for
                if (element[lens_code].Split(' ')[0] == "!")
                {
                    if (check_for == 0) { otp.Text = "Invalid syntax: ..."; }
                    lens_code = check_for;
                }

                //while
                if (element[lens_code].Split(' ')[0] == "while")
                {     
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
                    else { 
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
                    cls.run(element, lens_code, list_double, element[lens_code].Split(' ')[0]);
                }
                #endregion

                ++lens_code;
            }
            #region Error: checking brackets
            if (_sk > 1) { otp.Text = "Invalid syntax: missing closing brackets"; }
            else if (_sk > 0) { otp.Text = "Invalid syntax: missing closing bracket"; }
            else if (_sk == -1) { otp.Text = "Invalid syntax: missing opening bracket"; }
            else if (_sk < 0) { otp.Text = "Invalid syntax: missing opening brackets"; }
            #endregion
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear
            otp.Text = "";
            list_int.Clear();
            list_string.Clear();
            list_double.Clear();
            lens_code = 0;
            _sk = 0;
            //
            Run();
        }
    }
}
