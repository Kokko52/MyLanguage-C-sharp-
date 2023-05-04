﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MyLanguage
{
    public partial class MyLanguage : Form
    {
        //list int
        public Dictionary<string, int> val_int = new Dictionary<string, int>();

        //original string
        public string str;
        public string[] element = new string[] { };
        public MyLanguage()
        {
            InitializeComponent();
        }
        public void Run()
        {
            //clearing
            str = myCode.Text.Replace("\n", "").Trim();
            //seting elements string
            element = str.Split('\r');
            //removing spaces
            for (int i = 0; i < element.Length; i++)
            {
                element[i] = element[i].Trim();
            }

            //lens code
            int lens_code = 0;

            while (lens_code < element.Length - 1)
            {
                #region main func -KV
                //main func 'KV{'
                if (element[0] != "KV{")
                {
                    output.Text = $"Syntax error: main function not found - {element[0]}";
                    return;
                }
                //end main func - KV
                if (element[element.Length - 1] != "}")
                {
                    output.Text = "Syntax error: end main function not found";
                    return;
                }
                #endregion

                #region kv.print
                //if there is a keyword - kv.print
                if (element[lens_code].Split(' ')[0] == "kv.print")
                {
                    //kvprint kv = new kvprint();
                    //kv.str = element[lens_code];
                    //output.Text = kv.output();


                    string element;
                    //geting the value
                    element = str.Split('(')[1].Split(')')[0].Replace("\"", "");

                    if (val_int.ContainsKey(element))
                    {
                        output.Text = Convert.ToString(val_int[element]);
                    }
                    else
                    {
                        output.Text = element;
                    }

                }
                #endregion

                //kvint
                if (element[lens_code].Split(' ')[0] == "kvint")
                {
                    string name;
                    int volume = 0;

                    //
                    kvint kv = new kvint();
                    kv.str = element[lens_code].Trim();
                    //

                    //string variable + volume
                    string string_int = kv.adding_int();

                    //name variable
                    name = string_int.Split(' ')[0];

                    try
                    {
                        //volume variable
                        volume = Convert.ToInt32(string_int.Split(' ')[1]);
                    }
                    //Incorrect Format volume
                    catch (FormatException) { output.Text = $"incorrect value \'{string_int.Split(' ')[1]}\' for the variable - \'{name}\'"; return; }

                    try
                    {
                        //ading variable
                        val_int.Add(name, volume);
                    }

                    catch (ArgumentException) { output.Text = $"Variable \'{name}\' with the volume \'{volume}\' already exists"; return; }
                }
                ++lens_code;


            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output.Text = "";
            val_int.Clear();
            Run();
        }
        /*
         * KV{
     int.a = 12;
     int.b = 13;
     if(a > b)
    {
        print(9);
    }
}
         */
    }
}
