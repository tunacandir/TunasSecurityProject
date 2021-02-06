using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
//using static Java.Util.ResourceBundle;

namespace TunasSecurityProgram
{
    [Activity(Label = "Connect")]
    public class Connect : Activity
    {
        private EditText edtIp, edtport;
        private Button btnConnect;
        private TcpClient client;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            client = new TcpClient();
            // Create your application here  
            SetContentView(Resource.Layout.Connect);
            edtIp = FindViewById<EditText>(Resource.Id.edtIpAddress);
            edtport = FindViewById<EditText>(Resource.Id.edtPort);
            btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            btnConnect.Click += async delegate
            {
                try
                {
                    await client.ConnectAsync(edtIp.Text, Convert.ToInt32(edtport.Text));
                    if (client.Connected)
                    {
                        Connection.Instance.client = client;
                        Toast.MakeText(this, "Client connected to server!", ToastLength.Short).Show();
                        Intent intent = new Intent(this, typeof(Control));
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast.MakeText(this, "Connection feild!", ToastLength.Short).Show();
                    }
                }
                catch (Exception x)
                {
                    Toast.MakeText(this, "Connection feild!", ToastLength.Short).Show();
                    Toast.MakeText(this, "" + x, ToastLength.Short).Show();
                }
            };
        }
    }
}