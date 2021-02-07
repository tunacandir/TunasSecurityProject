using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Bu classda adminin kayıt esnasında vermesi gereken ve local veri tabanına 
 * kayıt olacak bilgileri belirtiliyor.
 */

namespace TunasSecurityProgram
{
    public class Admin 
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public Admin() { }
        public Admin(string Id, string username, string fullName, string email, string password, string mobile) //Tüm parametrelere sahip constructor 
        {
            ID = Id;
            Username = username;
            FullName = fullName;
            Email = email;
            Password = password;
            Mobile = mobile;
        }
        public Admin(string Password) //Parola doğrulaması için tek parametreye sahip constructor
        {
            this.Password = Password;
        }
    }
}