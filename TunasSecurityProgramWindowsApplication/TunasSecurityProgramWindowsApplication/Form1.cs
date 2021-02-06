using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace TunasSecurityProgramWindowsApplication
{
    public partial class Form1 : Form
    {
        //public staticler sayesinde bunların hepsini bütün formlarda kullanabiliyoruz
        public static int yuklenme = 20; // progress bar için ne kadar yükleneceğini tutan public static int.
        public static int dogru = 0; // dogru sayısı sayacı
        public static int yanlis = 0; // yanlış sayacı
        public static int saniye = 0; // saniye sayacı
        public static int dakika = 0; // dakika sayacı
        public static Form frm1 = new Form(); // form 1 yi kullanmak diğer formlarda kullanmaya yarar
        public static Form2 frm2 = new Form2(); // form 2 yi kullanmak diğer formlarda kullanmaya yarar
        public static Form3 frm3 = new Form3(); // form 3 yi kullanmak diğer formlarda kullanmaya yarar
        public static Form4 frm4 = new Form4(); // form 4 yi kullanmak diğer formlarda kullanmaya yarar
        public static Form5 frm5 = new Form5(); // form 5 yi kullanmak diğer formlarda kullanmaya yarar
        public static Form6 frm6 = new Form6(); // form 6 yi kullanmak diğer formlarda kullanmaya yarar
        public static Form7 frm7 = new Form7(); // form 7 yi kullanmak diğer formlarda kullanmaya yarar
        public static Form8 frm8 = new Form8(); // form 8 yi kullanmak diğer formlarda kullanmaya yarar

        const int WM_USER = 0x8000 + 1;
        ArrayList _al = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.background;
            FormBorderStyle = FormBorderStyle.None; //formu borderless yapar
            richTextBox1.BackColor = Color.Green;
            button1.Image = Properties.Resources.başla; //başlangıçta butona başla resmini yerleştir
            richTextBox1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm2.Show();
            this.Hide();

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_USER)
            {
                if (m.LParam != IntPtr.Zero)
                {
                    //add the byte to array list
                    _al.Add((byte)m.LParam);
                }
                else
                {
                    //end of message found
                    byte[] b = ((byte[])_al.ToArray(typeof(byte)));
                    string message = Encoding.UTF8.GetString(b);

                    if (message == "AuthenticationSuccesful")
                    {
                        button1.Enabled = true;
                        richTextBox1.AppendText(Environment.NewLine + DateTime.Today + "Authentication Succesful");
                    }
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
