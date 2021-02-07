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
//Servera bağlanmak için kullandığımız aktivite
namespace TunasSecurityProgram
{
    [Activity(Label = "Connect")]
    public class Connect : Activity
    {
        //Connect.xml de bulunan viewları değişkene atamak için belirlediğimiz değişkenler
        private EditText edtIp, edtport;
        private Button btnConnect;
        private TcpClient client;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //yeni bir TCP Clientı oluşturduk
            client = new TcpClient();
            //Sayfayı Connect.xml olarak açtık ve değişkenlere viewleri atadık
            SetContentView(Resource.Layout.Connect);
            edtIp = FindViewById<EditText>(Resource.Id.edtIpAddress);
            edtport = FindViewById<EditText>(Resource.Id.edtPort);
            btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            //Connect butonuna tıkladığımızda try catch çalışıyor
            btnConnect.Click += async delegate
            {
                try
                {
                    await client.ConnectAsync(edtIp.Text, Convert.ToInt32(edtport.Text)); //Clienti bağlamaya çalışıyor
                    if (client.Connected)//eğer bağlanırsa  Control sayfasına bağlanıyor.
                    {
                        Connection.Instance.client = client;
                        Toast.MakeText(this, "Client connected to server!", ToastLength.Short).Show();
                        Intent intent = new Intent(this, typeof(Control));
                        StartActivity(intent);
                    }
                    else //Bağlantının olamadığı ekrana pop up olarak gözüküyor
                    {
                        Toast.MakeText(this, "Connection failed!", ToastLength.Short).Show();
                    }
                }
                catch (Exception x) //Hata durumunda bağlantının olmadığını belirtiyor
                {
                    Toast.MakeText(this, "Connection failed!", ToastLength.Short).Show();
                    Toast.MakeText(this, "" + x, ToastLength.Short).Show();
                }
            };
        }
    }
}