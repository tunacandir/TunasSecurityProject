using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TunasSecurityProgramWindowsApplication
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None; //formu borderless yapar
            label1.Text = "!!!Dünyanın En Zor Testi!!!"; //labela yazı
            button1.Image = Properties.Resources.başla; //başlangıçta butona başla resmini yerleştir
            Form1.frm2.BackgroundImage = Properties.Resources.background;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.frm3.Show(); //2. formu açar
            this.Hide(); // bu formu saklar
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.başla2; //mouseun üzerine geldiğinde resmi başla 2 ye çevir
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.başla; // mouseu hoverdan normale çektiğinde tekrardan başla resmi açılır
        }
    }
}
