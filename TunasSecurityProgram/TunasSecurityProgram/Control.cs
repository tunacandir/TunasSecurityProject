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
        private Button btnTakeScreen, btnSleep, btnShutdown, btnLogout, btnBioAuth;
        private ImageView imageView;
        NetworkStream stream;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var client = Connection.Instance.client;
            // Create your application here
            SetContentView(Resource.Layout.Control);
            btnTakeScreen = FindViewById<Button>(Resource.Id.btnTakeScreen);
            btnSleep = FindViewById<Button>(Resource.Id.btnSleep);
            btnShutdown = FindViewById<Button>(Resource.Id.btnShutdown);
            btnBioAuth = FindViewById<Button>(Resource.Id.btnBioAuth);
            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            //Sleep command button  

            btnSleep.Click += delegate
            {
                stream = client.GetStream();
                String s = "SLP2";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            };

            //Shutdown command button  

            btnShutdown.Click += delegate
            {
                stream = client.GetStream();
                String s = "SHTD3";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            };

            //Auth Page command button

            btnBioAuth.Click += delegate
            {
                stream = client.GetStream();
                String s = "AUTH1";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
                StartActivity(typeof(BioAuthControl));
            };

            //Take Screenshot command button  

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

            //Logout button  

            btnLogout.Click += delegate
            {
                StartActivity(typeof(MainActivity));
                client.Close();
            };
        }

        //Convert byte to Image  

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
                    curDataSize = client.Available;//This save me  

                bytes = stream.Read(data, bytesRead, curDataSize);
                bytesRead += curDataSize;
                bytesLeft -= curDataSize;
            }
            return data;
        }
    }
}