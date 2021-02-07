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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e) //aşağıdaki kodlar form yüklenirken çalışmaya başlar
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; //formu borderless yapar
            progressBar1.Value = Form1.yuklenme; // progressbarın yeni değeri 80 olur
            Form1.yuklenme += 20; // progress bara yeni değer olarak 100 atar
            timer1.Interval = 1000; // bu formdaki timerın tickini 1 saniyedde bir çalıştırır
            timer1.Enabled = true; // bu fomrdaki timerı çalıştırır
            label2.Text = "Aşağıdakilerden hangisi veya hangileri programlama dilidir?"; // labela soruyu yazdırır
            label3.Text = Form1.dakika.ToString(); //label 3 e dakika sayacındaki sayıyı string olarak yazdırır
            label4.Text = Form1.saniye.ToString(); //label 4 e saniye sayacındaki sayıyı string olarak yazdırır
            checkBox1.Text = "SQL";
            checkBox2.Text = "Java";
            checkBox3.Text = "HTML";
            checkBox4.Text = "C#";
            //üstteki dört satır checkBox ların her birine farklı bir yazı yazdırır
            button1.Image = Properties.Resources.ilerle; // butona ilerle resmini yükle
            button1.Text = ""; // butonun yazısını sil
            label1.Image = Properties.Resources.soru5; // labela soru5 resmini getirir
        }

        private void label1_Click(object sender, EventArgs e)
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

            label3.Text = Form1.dakika.ToString(); //her 1 saniyede bir label 3 e yeni saniyeyi yaz
            label4.Text = Form1.saniye.ToString(); //her 1 saniyede bir label 4 ye yeni saniyeyi yaz
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true) //eğer checkboxların hepsi seçili ise
            {
                Form1.yanlis++; //yanlışı arttırın
                this.Hide(); //bu formu sakla
                Form1.frm8.Show(); // form 8i göster
            }
            else if (checkBox2.Checked == true && checkBox4.Checked == true && checkBox1.Checked == true) //checkbox 1 2 ve 4 seçili ise
            {
                Form1.yanlis++; //yanlış arttırır
                this.Hide(); //bu formu sakla
                Form1.frm8.Show(); // form8i göster
            }
            else if (checkBox2.Checked == true && checkBox4.Checked == true && checkBox3.Checked == true) //checkbox 2 3 ve 4 seçili ise
            {
                Form1.yanlis++; //yanlış arttırır
                this.Hide(); //bu formu sakla
                Form1.frm8.Show(); // form8i göster
            }
            else if (checkBox2.Checked == true && checkBox4.Checked == true) // check box 2 ve 4 seçiliyse
            {
                Form1.dogru++; // doğru sayacını arttır
                this.Hide(); //bu formu sakla
                Form1.frm8.Show(); // 8. formu göster
            }
            else
            {
                Form1.yanlis++; //eğer farklı bir şeyler seçili ise yanlış say
                this.Hide(); //bu formu sakla
                Form1.frm8.Show(); // form8i göster
            }


            timer1.Enabled = false; // bu formdaki timerı kapatır
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.ilerle2; //butonun üzerine gelince resmi değiştir
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.ilerle; //butonun üzerinden çıkınca resmi eski resimle değiştir
        }
    }
}
