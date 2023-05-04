using System;
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
        #region Storages Variables
        //list int
        public Dictionary<string, int> val_int = new Dictionary<string, int>();

        //list string
        public Dictionary<string, string> val_string = new Dictionary<string, string>();

        //list float
        public Dictionary<string, float> val_float = new Dictionary<string, float>();

        //list double
        public Dictionary<string, double> val_double = new Dictionary<string, double>();

        //list char
        public Dictionary<string, char> val_char = new Dictionary<string, char>();

        //list bool
        public Dictionary<string, bool> val_bool = new Dictionary<string, bool>();
        #endregion

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
                    //value
                    string element;

                    //geting the value
                    element = str.Split('(')[1].Split(')')[0].Replace("\"", "");

                    //if the variable exists
                    if (val_int.ContainsKey(element))
                    {
                        output.Text = Convert.ToString(val_int[element]);
                    }
                    else if (val_string.ContainsKey(element))
                    {
                        output.Text = Convert.ToString(val_string[element]);
                    }
                    //else print text
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

                    //name variable
                    name = element[lens_code].Trim().Split(' ')[1].Split(' ')[0];
                    //volume
                    try
                    {
                        //volume variable
                        volume = Convert.ToInt32(element[lens_code].Trim().Split('=')[1].Trim());
                    }
                    //Incorrect Format volume
                    catch (FormatException) { output.Text = $"incorrect value \'{element[lens_code].Trim().Split('=')[1].Trim()}\' for the variable - \'{name}\'"; return; }
                    //add
                    try
                    {
                        //ading variable
                        val_int.Add(name, volume);
                    }
                    //The Exists
                    catch (ArgumentException) { output.Text = $"Variable \'{name}\' with the volume \'{volume}\' already exists"; return; }
                }
                //kvstring
                if (element[lens_code].Split(' ')[0] == "kvstring")
                {
                    string name;
                    string volume;

                    //name variable
                    name = element[lens_code].Trim().Split(' ')[1].Split(' ')[0];
                    //volume
                    try
                    {
                        //volume variable
                        volume = element[lens_code].Trim().Split('=')[1].Trim();
                    }
                    //Incorrect Format volume
                    catch (FormatException) { output.Text = $"incorrect value \'{element[lens_code].Trim().Split('=')[1].Trim()}\' for the variable - \'{name}\'"; return; }
                    //add
                    try
                    {
                        //ading variable
                        val_string.Add(name, volume);
                    }
                    //The Exists
                    catch (ArgumentException) { output.Text = $"Variable \'{name}\' with the volume \'{volume}\' already exists"; return; }
                }


                ++lens_code;
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output.Text = "";
            val_int.Clear();
            val_string.Clear();
            Run();
        }
    }
}
