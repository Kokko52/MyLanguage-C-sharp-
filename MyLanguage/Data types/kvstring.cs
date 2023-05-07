﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Linq;
using System.Windows.Forms;

namespace MyLanguage
{
    class kvstring
    {
        //kvint string
        public string str;
        //element string kvstring
        public string name;
        //volume string kvstring
        public string volume;

        public bool run(Dictionary<string, int>  list_int, Dictionary<string, string> list_string, Dictionary<string, double> list_double, TextBox otp)
        {
            //seting name and volume
            name = str.Split(' ')[1].Split(' ')[0];
            volume = str.Split('=')[1].Trim();
            //

            //if variable exists
            if (list_int.ContainsKey(name) || list_string.ContainsKey(name) || list_double.ContainsKey(name))
            {
                otp.Text = $"Variable \'{name}\' with the volume \'{volume}\' already exists";
                return false;
            }
            //else add variable
            else
            {
                //add variable
                list_string.Add(name, volume);
            }

            return true;
        }
    }
}