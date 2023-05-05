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
            int lens_code = 0;
            while (lens_code < element.Length - 1)
            {
                //kv.print
                if (element[lens_code].Split(' ')[0] == "kv.print")
                {
                    kvprint kvprint = new kvprint();
                    kvprint.str = element[lens_code].Trim();
                    if (!kvprint.run(list_int, list_string, list_double, otp)) { break; }
                }

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

                //ifik
                if ( element[lens_code].Split(' ')[0] == "ifik")
                {
                    ifik ifik = new ifik();
                    ifik.lens_code = lens_code;
                    ifik.str = element[lens_code].Trim().Replace(" ", "");
                    if (!ifik.run(list_int, list_string, list_double, otp, element)) { break; }
                    lens_code = ifik.lens_code;
                }                 
                ++lens_code;
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear
            otp.Text = "";
            list_int.Clear();
            list_string.Clear();
            list_double.Clear();
            lens_code = 0;
            //
            Run();
        }
    }
}
