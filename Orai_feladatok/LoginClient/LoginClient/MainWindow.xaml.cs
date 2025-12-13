using LoginClient.Models;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string CreateSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();

            }

        }
        private static List<User> users = new List<User>();
        string token = string.Empty;


        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new("https://localhost:7030/")
        };
      public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SaltResponse = client.GetStringAsync($"/Login/GetSalt?loginName={tbxLoginNev.Text}");
                if (SaltResponse != null)
                {
                    MessageBox.Show(SaltResponse.Result);
                    string hashed = CreateSHA256(pbxLogin.Password + SaltResponse.Result);
                    var loginResponse  = client.PostAsync("Login/Login" , new )
                }
                else
                {
                    MessageBox.Show("Nincs só ! Sikertelen bejelentekezés");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}