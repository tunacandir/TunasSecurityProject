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

namespace TunasSecurityProgram
{
    public class Connection
    {
        private static Connection _instance;

        public static Connection Instance
        {
            get
            {
                if (_instance == null) _instance = new Connection();
                return _instance;
            }
        }
        public TcpClient client { get; set; }
    }
}