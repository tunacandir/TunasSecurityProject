using System;
using Android;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;
using Xamarin.Essentials;
using System.Net.Sockets;
using System.Text;

namespace TunasSecurityProgram
{
    internal class FingerprintHandler : FingerprintManager.AuthenticationCallback
    {
        private Context mainActivity;
        NetworkStream stream;
        public FingerprintHandler(Context mainActivity)
        {
            this.mainActivity = mainActivity;
        }
        internal void StartAuthentication(FingerprintManager fingerprintManager, FingerprintManager.CryptoObject cryptoObject)
        {
            CancellationSignal cancellationSignal = new CancellationSignal();
            if (ActivityCompat.CheckSelfPermission(mainActivity, Manifest.Permission.UseFingerprint)!= (int)Android.Content.PM.Permission.Granted)
            {
                return;
            }
            fingerprintManager.Authenticate(cryptoObject, cancellationSignal, 0, this, null);
        }
        public override void OnAuthenticationFailed()
        {
            var client = Connection.Instance.client;
            stream = client.GetStream();
            String s = "AUTH2";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);

            Vibration.Vibrate();
            var duration = TimeSpan.FromSeconds(1);
            Vibration.Vibrate(duration);

            Toast.MakeText(mainActivity, "Fingerprint Authentication failed!", ToastLength.Long).Show();
            mainActivity.StartActivity(new Intent(mainActivity, typeof(Control)));
        }
        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {
            var client = Connection.Instance.client;
            stream = client.GetStream();
            String s = "AUTH3";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);

            Vibration.Vibrate();
            var duration = TimeSpan.FromSeconds(1);
            Vibration.Vibrate(duration);

            Toast.MakeText(mainActivity, "Fingerprint Authentication Succeeded!", ToastLength.Long).Show();
            mainActivity.StartActivity(new Intent(mainActivity, typeof(Control)));
        }
    }
}