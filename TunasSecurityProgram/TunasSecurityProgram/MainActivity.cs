using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Database.Sqlite;

namespace TunasSecurityProgram
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //resourcesın altındaki layoutun altındaki activity_main.xml inde bulunan nesnelerin tanımlanması
        private EditText txtUsername, txtPassword;
        private Button btnSignIn, btnCreate;
        Helper helper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Görünümümüzü "Main" e ayarlıyoruz
            SetContentView(Resource.Layout.activity_main);
            //activity_main.xml den viewları bulup objelerimize atıyoruz
            txtUsername = FindViewById<EditText>(Resource.Id.txtusername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtpassword);
            btnCreate = FindViewById<Button>(Resource.Id.btnSignUp);
            btnSignIn = FindViewById<Button>(Resource.Id.btnSign);
            helper = new Helper(this);
            //Eğer yeni kullanıcı create etmek istersek Signup sayfasına yönlendiriliyoruz
            btnCreate.Click += delegate { StartActivity(typeof(SignUp)); };
            //Girişdekayıt esnasında belirtilen username ve password doğru girilirse Connect activitesi başlatılıyor
            //eğer yanlış ise uyarı veriyor
            btnSignIn.Click += delegate
            {
                try
                {
                    string Username = txtUsername.Text.ToString();
                    string Password = txtPassword.Text.ToString();
                    var user = helper.Authenticate(this, new Admin(null, Username, null, null, Password, null));
                    if (user != null)
                    {
                        Toast.MakeText(this, "Login Successful", ToastLength.Short).Show();
                        StartActivity(typeof(Connect));
                    }
                    else
                    {
                        Toast.MakeText(this, "Login Unsuccessful! Please verify your Username and Password", ToastLength.Short).Show();
                    }
                }
                catch (SQLiteException ex)
                {
                    Toast.MakeText(this, "" + ex, ToastLength.Short).Show();
                }

            };
        }
    }
}