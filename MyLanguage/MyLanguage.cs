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
                    output.Text = $"Syntax error: end main function not found";
                    return;
                }
                #endregion
                //kvint
                if (element[lens_code].Length > 5)
                {
                    //if there is a keyword - kvint
                    if (element[lens_code].Split(element[lens_code][5])[0] == "kvint")
                    {
                        kvint kv = new kvint();
                        kv.str = element[lens_code];
                        output.Text = kv.output();
                    }
                }
                ++lens_code;
            }


        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //myCode.Text = "";
            output.Text = "";
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