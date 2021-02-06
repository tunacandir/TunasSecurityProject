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
        private EditText txtUsername, txtPassword;
        private Button btnSignIn, btnCreate;
        Helper helper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_main);

            txtUsername = FindViewById<EditText>(Resource.Id.txtusername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtpassword);
            btnCreate = FindViewById<Button>(Resource.Id.btnSignUp);
            btnSignIn = FindViewById<Button>(Resource.Id.btnSign);
            helper = new Helper(this);

            btnCreate.Click += delegate { StartActivity(typeof(SignUp)); };

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