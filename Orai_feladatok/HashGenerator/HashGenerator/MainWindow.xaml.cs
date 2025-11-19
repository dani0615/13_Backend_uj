using Microsoft.Win32;
using System.IO;
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

namespace HashGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateSalt();
        }

        public static string GenerateSalt() 
        {
            Random random = new Random();
            string karakterek = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string salt = "";
            for (int i =0; i< 64; i++)
            {
                int index = random.Next(karakterek.Length);
                salt += karakterek[index];
            }
            return salt;
           
        }

        private void btnGeneralas_Click(object sender, RoutedEventArgs e)
        {
            string salt = GenerateSalt();
            tbxSalt.Text = salt;
            string elokeszitve = tbxPassword.Text + salt;
            tbxHash.Text =CreateSHA256(elokeszitve);
            tbxDoubleHash.Text = CreateSHA256(tbxHash.Text + salt);
            

        }

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

        private void Menu_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("hash.txt");
            sw.WriteLine(tbxLogin.Text);
            sw.WriteLine(tbxPassword.Text);
            sw.WriteLine(tbxHash.Text);
            sw.WriteLine(tbxDoubleHash.Text);
            sw.Close();
          
            
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Menu_About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hash Generator Pro v1.0\n\nKészítette: Dragon\nBiztonságos jelszóhash generátor SHA-256 algoritmussal.",
                            "Névjegy", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}