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
        #region textBoxs
        //textBox1 - code
        public TextBox code = new TextBox();
        //textBox1 - code
        public TextBox otp = new TextBox();
        #endregion
        #region Storages Variables
        //list int
        public Dictionary<string, int> list_int = new Dictionary<string, int>();

        //list string
        public Dictionary<string, string> list_string = new Dictionary<string, string>();

        //list float
      //  public Dictionary<string, float> val_float = new Dictionary<string, float>();

        //list double
        public Dictionary<string, double> list_double = new Dictionary<string, double>();

        //list char
        //public Dictionary<string, char> val_char = new Dictionary<string, char>();

        //list bool
       // public Dictionary<string, bool> val_bool = new Dictionary<string, bool>();
        #endregion

        //original string
        public string str;
        //lines
        public string[] element = new string[] { };
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

            //lens code
            int lens_code = 0;

            while (lens_code < element.Length - 1)
            {
                #region main func - 'KV'
                //main func 'KV{'
                if (element[0] != "KV{")
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

                #region kv.print
                //if there is a keyword - kv.print
                if (element[lens_code].Split(' ')[0] == "kv.print")
                {
                    //value
                    string element;

                    //geting the value
                    element = str.Split('(')[1].Split(')')[0].Replace("\"", "");

                    //if the variable exists
                    if (list_int.ContainsKey(element))
                    {
                        otp.Text = Convert.ToString(list_int[element]);
                    }
                    else if (list_string.ContainsKey(element))
                    {
                        otp.Text = Convert.ToString(list_string[element]);
                    }
                    //else print text
                    else
                    {
                        otp.Text = element;
                    }
                }
                #endregion
           
                //kvint
                if (element[lens_code].Split(' ')[0] == "kvint")
                {
                    kvint kvint = new kvint();
                    kvint.str = element[lens_code].Trim();
                    if(!kvint.run(list_int, list_string, list_double, otp)) { break;}


                    //string name;
                    //int volume = 0;
                    //kvint kv = new kvint();
                    //kv.str = str.Trim();
                    ////name variable
                    //name = element[lens_code].Trim().Split(' ')[1].Split(' ')[0];
                    ////volume
                    //try
                    //{
                    //    //volume variable
                    //    volume = Convert.ToInt32(element[lens_code].Trim().Split('=')[1].Trim());
                    //}
                    ////Incorrect Format volume
                    //catch (FormatException) { otp.Text = $"incorrect value \'{element[lens_code].Trim().Split('=')[1].Trim()}\' for the variable - \'{name}\'"; return; }
                    ////add
                    //try
                    //{
                    //    //ading variable
                    //    list_int.Add(name, volume);
                    //}
                    ////The Exists
                    //catch (ArgumentException) { otp.Text = $"Variable \'{name}\' with the volume \'{volume}\' already exists"; return; }
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
                    catch (FormatException) { otp.Text = $"incorrect value \'{element[lens_code].Trim().Split('=')[1].Trim()}\' for the variable - \'{name}\'"; return; }
                    //add
                    try
                    {
                        //ading variable
                        list_string.Add(name, volume);
                    }
                    //The Exists
                    catch (ArgumentException) { otp.Text = $"Variable \'{name}\' with the volume \'{volume}\' already exists"; return; }
                }


                ++lens_code;
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            otp.Text = "";
            list_int.Clear();
            list_string.Clear();
            Run();
        }
    }
}
