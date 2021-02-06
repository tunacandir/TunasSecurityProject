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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e) //aşağıdaki kodlar form yüklenirken çalışmaya başlar
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; //formu borderless yapar
            progressBar1.Value = Form1.yuklenme; //progres bara 20 değerini verir
            Form1.yuklenme += 20; //önümüzdeki form için progres barın yuklenme değerine 20 ekler
            timer1.Interval = 1000; // timerın saniyede bir çalışmasını sağlar
            timer1.Enabled = true; //timerın çalışmasını sağlar
            label3.Text = Form1.dakika.ToString(); //dakika sayacındaki tutulan sayıyı label3 e atar
            label4.Text = Form1.saniye.ToString(); //saniye sayacındaki tutulan sayıyı label4 e atar
            button1.Image = Properties.Resources.ilerle; // butona ilerle resmini yükle
            button1.Text = ""; // butonun yazısını sil
            label1.Image = Properties.Resources.soru1; //labela soru1  resmini yükle
        }

        private void button1_Click(object sender, EventArgs e) // butona basınca olacak olaylar
        {
            if (radioButton1.Checked)
            {
                Form1.dogru++;// eğer birinci radio buton seçili ise dogru sayacını arttırır
            }
            else
            {
                Form1.yanlis++; // eğer diğerleri seçili ise veya seçilmeyen varsa yanlş sayacını arttırır
            }
            Form1.frm4.Show(); // üçüncü formu  açar
            this.Hide(); // bu formu saklar
            timer1.Enabled = false; // bu formadaki timeri etkisiz hale getirir
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e) // timerin her tickinde (1 saniyede bir çünkü interval 1000) çalışır
        {
            Form1.saniye++; //saniyeyi bir arttır 
            if (Form1.saniye > 60) //eğer saniye sayacı 60 ın üstindeyse  (bunu yapma sebebim saniyenin hata ile 60ın üzerinne çıkarsa düzelmesi için)
            {
                Form1.dakika++; // dakikayı bir arttır
                Form1.saniye = Form1.saniye - 60; //saniyeden 60 çıkartarak kalan saniyeyi bul
            }
            else if (Form1.saniye == 60) // eğer saniye 60a eşit ise
            {
                Form1.dakika++; //dakikayı 1 arttır
                Form1.saniye = 0; //saniyeyi 0 yap
            }

            label3.Text = Form1.dakika.ToString(); //her 1 saniyede bir label 3 e yeni dakikayı yaz
            label4.Text = Form1.saniye.ToString(); //her 1 saniyede bir label 4 e yeni saniyeyi yaz

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.ilerle2; //butonun üzerine gelince resmi değiştir
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.ilerle; //butonun üzerinden çıkınca resmi eski resimle değiştir
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
