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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e) //aşağıdaki kodlar form yüklenirken çalışmaya başlar
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; //formu borderless yapar
            progressBar1.Value = Form1.yuklenme; // progress bara yeni değer olarak 40 atar
            label1.Text = Form1.dakika.ToString(); //labela hafızada olan dakikayı atar
            label2.Text = Form1.saniye.ToString(); //labela hafızada olan saniyeyi atar
            Form1.yuklenme += 20; // yeni yuklenme değerini 60 yapar
            timer1.Interval = 1000; // bu formdaki timerın saniyesini
            timer1.Enabled = true; // timerşn çalışmasını sağlar
            checkBox1.Text = "Ay bir yıldızdır.";
            checkBox2.Text = "Güneş bir yıldızdır.";
            checkBox3.Text = "Dünya bir gezegendir.";
            checkBox4.Text = "Dünya bir yıldızdır.";
            //üstteki dört kod checkboxların içeriğini yazar
            button1.Image = Properties.Resources.ilerle; // butona ilerle resmini yükle
            button1.Text = ""; // butonun yazısını sil
            label3.Image = Properties.Resources.soru2; // labela soru2 resmini getirir
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

            label1.Text = Form1.dakika.ToString(); //her 1 saniyede bir label 1 e yeni saniyeyi yaz
            label2.Text = Form1.saniye.ToString(); //her 1 saniyede bir label 2 ye yeni saniyeyi yaz
        }



        private void button1_Click(object sender, EventArgs e) // butona basıldığında oalcaklar
        {
            if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true) //eğer checkboxların hepsi seçili ise
            {
                Form1.yanlis++; //yanlışı arttırın
                this.Hide(); //bu formu sakla
                Form1.frm5.Show(); // form 5i göster
            }
            else if (checkBox2.Checked == true && checkBox3.Checked == true && checkBox1.Checked == true) //checkbox 1 2 ve 3 seçili ise
            {
                Form1.yanlis++; //yanlış arttırır
                this.Hide(); //bu formu sakla
                Form1.frm5.Show(); // form5i göster
            }
            else if (checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true) //checkbox 2 3 ve 4 seçili ise
            {
                Form1.yanlis++; //yanlış arttırır
                this.Hide(); //bu formu sakla
                Form1.frm5.Show(); // form5i göster
            }
            else if (checkBox2.Checked == true && checkBox3.Checked == true) // check box 2 ve 3 seçiliyse
            {
                Form1.dogru++; // doğru sayacını arttır
                this.Hide(); //bu formu sakla
                Form1.frm5.Show(); // 5. formu göster
            }
            else
            {
                Form1.yanlis++; //eğer farklı bir şeyler seçili ise yanlış say
                this.Hide(); //bu formu sakla
                Form1.frm5.Show(); // form5i göster
            }

            timer1.Enabled = false; //timerı gizle
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
