using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Java.Security;
using Javax.Crypto;
using Android.Hardware.Fingerprints;
using Android.Support.V4.App;
using Android;
using System;
using Android.Security.Keystore;
using System.Net.Sockets;
using System.Text;

namespace TunasSecurityProgram
{
    [Activity(Label = "BioAuthControl", Theme = "@style/AppTheme")] //, Theme = "@style/AppTheme"
    public class BioAuthControl : Activity
    {
        private Button btnGoBack;
        private KeyStore keyStore;
        private Cipher cipher;
        private string KEY_NAME = "Tuna";
        NetworkStream stream;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BioAuthControl);
            KeyguardManager keyguardManager = (KeyguardManager)GetSystemService(KeyguardService);
            FingerprintManager fingerprintManager = (FingerprintManager)GetSystemService(FingerprintService);
            var client = Connection.Instance.client;
            btnGoBack = FindViewById<Button>(Resource.Id.btnGoBack);

            btnGoBack.Click += delegate
            {
                stream = client.GetStream();
                String s = "AUTH4";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
                StartActivity(typeof(Control));
            };

            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.UseFingerprint) != (int)Android.Content.PM.Permission.Granted)
            {
                return;
            }
                
            if (!fingerprintManager.IsHardwareDetected)
            {
                Toast.MakeText(this, "FingerPrint authentication permission not enable", ToastLength.Short).Show();
            }
            else
            {
                if (!fingerprintManager.HasEnrolledFingerprints)
                {
                    Toast.MakeText(this, "Register at least one fingerprint in Settings", ToastLength.Short).Show();
                } 
                else
                {
                    if (!keyguardManager.IsKeyguardSecure)
                        Toast.MakeText(this, "Lock screen security not enable in Settings", ToastLength.Short).Show();
                    else
                        GenKey();
                    if (CipherInit())
                    {
                        FingerprintManager.CryptoObject cryptoObject = new FingerprintManager.CryptoObject(cipher);
                        FingerprintHandler handler = new FingerprintHandler(this);
                        handler.StartAuthentication(fingerprintManager, cryptoObject);
                    }
                }
            }
        }
        private bool CipherInit()
        {
            try
            {
                cipher = Cipher.GetInstance(KeyProperties.KeyAlgorithmAes
                    + "/"
                    + KeyProperties.BlockModeCbc
                    + "/"
                    + KeyProperties.EncryptionPaddingPkcs7);
                keyStore.Load(null);
                IKey key = (IKey)keyStore.GetKey(KEY_NAME, null);
                cipher.Init(CipherMode.EncryptMode, key);
                return true;
            }
            catch (Exception ex) { return false; }
        }
        private void GenKey()
        {
            keyStore = KeyStore.GetInstance("AndroidKeyStore");
            KeyGenerator keyGenerator = null;
            keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, "AndroidKeyStore");
            keyStore.Load(null);
            keyGenerator.Init(new KeyGenParameterSpec.Builder(KEY_NAME, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                .SetBlockModes(KeyProperties.BlockModeCbc)
                .SetUserAuthenticationRequired(true)
                .SetEncryptionPaddings(KeyProperties
                .EncryptionPaddingPkcs7).Build());
            keyGenerator.GenerateKey();
        }
    }
}