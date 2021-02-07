using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TunasSecurityProgram
{
    [Activity(Label = "Control")]
    public class Control : Activity
    {
        //Control.xml deki viewler için değişken belirliyoruz
        private Button btnTakeScreen, btnSleep, btnShutdown, btnLogout, btnBioAuth;
        private ImageView imageView;
        NetworkStream stream;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var client = Connection.Instance.client;
            // Viewleri değişkenlere atıyoruz
            SetContentView(Resource.Layout.Control);
            btnTakeScreen = FindViewById<Button>(Resource.Id.btnTakeScreen);
            btnSleep = FindViewById<Button>(Resource.Id.btnSleep);
            btnShutdown = FindViewById<Button>(Resource.Id.btnShutdown);
            btnBioAuth = FindViewById<Button>(Resource.Id.btnBioAuth);
            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            //Uykuya geçmek için clientdan servera SLP2 diye bir string yoluyoruz 

            btnSleep.Click += delegate
            {
                stream = client.GetStream();
                String s = "SLP2";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            };

            //Kapatmak için clientdan servera SHTD3 diye bir string yoluyoruz 

            btnShutdown.Click += delegate
            {
                stream = client.GetStream();
                String s = "SHTD3";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            };

            //Parmak izi kontrolü için BioAuthControl sayfasına giriş yapıyoruz ve AUTH1 kodunu servera yolluyoruz

            btnBioAuth.Click += delegate
            {
                stream = client.GetStream();
                String s = "AUTH1";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
                StartActivity(typeof(BioAuthControl));
            };

            //Ekran görüntüsünü almak için TSC1 diye bir kod yolluyoruz gelen byteleri image a dönüştürüp ekranda gösteriyoruz bu işlem biraz uzun sürebiliyor 

            btnTakeScreen.Click += delegate
            {
                stream = client.GetStream();
                String s = "TSC1";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
                var data = getData(client);
                var image = BitmapFactory.DecodeByteArray(data, 0, data.Length);
                imageView.SetImageBitmap(image);
            };

            //Clienti kapatıp ana sayfaya aktarıyor

            btnLogout.Click += delegate
            {
                StartActivity(typeof(MainActivity));
                client.Close();
            };
        }

        //Serverdan gelen ekran görüntüsü byte olarak geliyor bunları image a dönüştürüyoruz 

        public byte[] getData(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] fileSizeBytes = new byte[4];
            int bytes = stream.Read(fileSizeBytes, 0, fileSizeBytes.Length);
            int dataLength = BitConverter.ToInt32(fileSizeBytes, 0);

            int bytesLeft = dataLength;
            byte[] data = new byte[dataLength];

            int buffersize = 1024;
            int bytesRead = 0;

            while (bytesLeft > 0)
            {
                int curDataSize = Math.Min(buffersize, bytesLeft);
                if (client.Available < curDataSize)
                    curDataSize = client.Available;

                bytes = stream.Read(data, bytesRead, curDataSize);
                bytesRead += curDataSize;
                bytesLeft -= curDataSize;
            }
            return data;
        }
    }
}