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
    public partial class Form8 : Form
    {
        public static int puan = 10; // skoru bulabilmek için kullanıldı
        public static int sure = 60; // skoru bulabilmek için kullanıldı
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e) //aşağıdaki kodlar form yüklenirken çalışmaya başlar
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; //formu borderless yapar
            label1.Text = "Toplam geçen süre:" + Form1.dakika + " dakika, " + Form1.saniye + "saniye"; //label a geçen dakika ve saniyeyi yazdırır
            label2.Text = "Doğru : " + Form1.dogru; //labela doğru sayısını yazdırır
            label3.Text = "Yanlış : " + Form1.yanlis; //labela yanlış sayısını yazdırır
            button1.Image = Properties.Resources.bitir; // butonun üzerine bitir resmini koyar
            if (Form1.yanlis == 5) // eğer her şeyi yanlış yaptıysan
            {
                puan = 0; //puan sıfırlanır
            }
            else //değilse
            {
                puan = puan * Form1.dogru; //her doğruyu için on puan eklenir
            }

            sure = sure * Form1.dakika; //dakikayı saniyeye çevirir
            sure = sure + Form1.saniye; //dakikadan gelen saniyeye elimizde tuttuğumuz saniyeyi ekleriz

            sure = 50 - sure; // elimizdeki süreyi 50 den çıkarırız
            puan = puan + sure;
            /* Üstteki satırın açıklaması eğer testi çözen kişi testi elli saniyenin altında bitirdiyse geri kalan her saniye için 1 puan kazanır
             fakat bitiremediyse 50 saniyenin üstüne geçen her saniyede 1 puan kaybeder taki kaybedecek puan kalmayana kadar*/
            if (Form1.yanlis == 5) //tekrardan kontrol ederek eğer hepsi yanlışsa 
            {
                puan = 0; //puanı sıfırlar
            }

            if (puan < 0) // eğer puan 0 ın altına düşmüşse
            {
                puan = 0; // puanı sıfırlar
            }

            label4.Text = "Skor : " + puan + "/100"; //label 4 e skorunuzu yazar

        }

        private void button1_Click(object sender, EventArgs e) // butona basınca olacakalar
        {
            Environment.Exit(0); // tüm uygulamayı kapatır
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.bitir2; //butonun üzerine gelince resmi değiştir
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.bitir; //butonun üzerinden çıkınca resmi eski resimle değiştir
        }
    }
}
