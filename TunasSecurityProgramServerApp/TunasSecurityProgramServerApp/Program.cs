using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TunasSecurityProgramServerApp
{
    public class Program
    {
        //Xamarin uygulaması ile iletişimde kullanılan değişkenler
        public static TcpClient client;
        private static TcpListener listener;
        private static string ipString;

        //Windows içerisinde iletişim kurabilmek için kullanılan değişkenler
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);

        public const int WM_USER = 0x8000 + 1;
        private static IntPtr _hwnd = IntPtr.Zero;
        private static string _procName = "TunasSecurityProgramWindowsApplication";


        static void Main(string[] args)
        {
            /* 
            Program bilgisayarın local Ip adresini çekiyor ve benim programda belirttiğim portta server olarak çalışmaya başlıyor.
            Daha sonra telefon uygulamasından bağlandığınızda telefeon uygulamasından bir listenera bir istek gidiyor ve server ile uygulama birbirine bağlanıyor.
            Client yani telefon, servera belirli kodları string olarak yolluyor ve eğer server bunları algılarsa try catch buloğu içerindeki if, elseler ile kontrolü yapılıp
            gerekli işlemleri sağlıyor.
            */
            IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in localIp)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipString = address.ToString();
                }
            }
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipString), 1234);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine(@"    
            ===================================================    
             Started listening requests at: {0}:{1}    
            ===================================================",
            ep.Address, ep.Port);
            client = listener.AcceptTcpClient();
            Console.WriteLine("Connected to client!" + " \n");
            while (client.Connected)
            {
                try
                {
                    const int bytesize = 1024 * 1024;
                    byte[] buffer = new byte[bytesize];
                    string x = client.GetStream().Read(buffer, 0, bytesize).ToString();
                    var data = ASCIIEncoding.ASCII.GetString(buffer);

                    if (data.ToUpper().Contains("SLP2"))
                    {
                        Console.WriteLine("Pc is going to Sleep Mode!" + " \n");
                        Sleep();
                    }
                    else if (data.ToUpper().Contains("SHTD3"))
                    {
                        Console.WriteLine("Pc is going to Shutdown!" + " \n");

                        Shutdown();
                    }
                    else if (data.ToUpper().Contains("TSC1"))
                    {
                        Console.WriteLine("Take Screenshot!" + " \n");

                        var bitmap = SaveScreenshot();
                        var stream = new MemoryStream();
                        bitmap.Save(stream, ImageFormat.Bmp);
                        sendData(stream.ToArray(), client.GetStream());
                    }
                    else if (data.ToUpper().Contains("AUTH1"))
                    {
                        Console.WriteLine("Authentication Started!" + " \n");
                    }
                    else if (data.ToUpper().Contains("AUTH2"))
                    {
                        Console.WriteLine("Authentication Failed!" + " \n");
                    }
                    else if (data.ToUpper().Contains("AUTH3")) //Telefondan bu kod gelirse AuthenticationSuccesful kodu eğer ismi belirtilen uygulama penceresi açıksa ona yollanır.
                    {
                        Console.WriteLine("Authentication Succeeded!" + " \n");
                        Process[] procs = Process.GetProcessesByName(_procName);
                        if (procs.Length > 0)
                        {
                            TargetHwnd = procs[0].MainWindowHandle; //Target pencerenin adını alıyoruz
                        }

                        if (TargetHwnd == IntPtr.Zero) //eğer adı yoksa 
                        {
                            Console.WriteLine("Window not found!");
                        }
                        else
                        {
                            Console.WriteLine("Window has found!");
                            SendString("AuthenticationSuccesful"); // Bilgisayarın içerisinden hedef pencereye bilgi gönderiliyor.
                        }
                    }
                    else if (data.ToUpper().Contains("AUTH4"))
                    {
                        Console.WriteLine("Authentication Canceled!" + " \n");
                    }
                    else if (data.ToUpper().Contains("APP1"))
                    {
                        Console.WriteLine("App is connected!" + " \n");
                    }
                }
                //Bir hata olması durumunda client kapatılır
                catch (Exception exc)
                {
                    client.Dispose();
                    client.Close();
                }
            }

            //Masaüstümüzü kapatmamız için kullandığımız fonksiyon

            void Shutdown()
            {
                System.Diagnostics.Process.Start("Shutdown", "-s -t 10");
            }

            //Masaüstümüzü uyku moduna sokmak için kullandığımız fonksiyon
            void Sleep()
            {
                Application.SetSuspendState(PowerState.Suspend, true, true);
            }

            //Ekran görüntüsü almaya yarayan fonksiyon. Görüntüyü bitmap olarak saklar 

            Bitmap SaveScreenshot()
            {
                var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                               Screen.PrimaryScreen.Bounds.Height,
                                               PixelFormat.Format32bppArgb);

                //Bitmapden grafik bir obje oluşturur 

                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                //Ekran görüntüsünü sol üst köşeden sağ alt köşeye alır

                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                return bmpScreenshot;
            }

            //Görüntüyü bayt tipi verilere dönüştürür
            void sendData(byte[] data, NetworkStream stream)
            {
                int bufferSize = 1024;
                byte[] dataLength = BitConverter.GetBytes(data.Length);

                stream.Write(dataLength, 0, 4);

                int bytesSent = 0;
                int bytesLeft = data.Length;

                while (bytesLeft > 0)
                {
                    int curDataSize = Math.Min(bufferSize, bytesLeft);

                    stream.Write(data, bytesSent, curDataSize);

                    bytesSent += curDataSize;
                    bytesLeft -= curDataSize;
                }
            }
        }

        //Windows uygulamalarının arasında anlaşmasında kullanacağımız string yollama fonksiyonumuz
        private static void SendString(string sMsg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(sMsg);
            foreach (byte b in bytes)
            {
                SendMessage(TargetHwnd, WM_USER, 38, b);
            }

            //Terminate edecek karakter yollayarak alıcının diziyi okuması durdurulur
            SendMessage(TargetHwnd, WM_USER, 38, 0);
        }

        //Target pencereyi belirtmek için kullanılır
        private static IntPtr TargetHwnd
        {
            get { return _hwnd; }
            set { _hwnd = value; }
        }
    }
}
