using CegautokKliens.UserManagement;
using System.Net.Http;
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

namespace CegautokKliens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient()
        {
            // A backend a launchSettings.json szerint a 7030-as (https) vagy 5222-es (http) porton figyel.
            BaseAddress = new("https://localhost:7030/")
        };

        public static string token = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuLogin_Click(object sender, RoutedEventArgs e)
        {
            if (token == null)
            {
                LoginWindow loginWindow = new LoginWindow(client);
                loginWindow.ShowDialog();
                if (token != null)
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    itemLogin.Header = "Logout";
                    MessageBox.Show("Sikeres bejelentkezés!");
                }
                else
                {
                    MessageBox.Show("Sikertelen bejelentkezés!");
                }
            }
            else
            {
                token = null;
                client.DefaultRequestHeaders.Remove("Authorization");
                MessageBox.Show("Kijelentkezett a szerverről!");
                itemLogin.Header = "Login";
            }
        }

        private void itemUserList_Click(object sender, RoutedEventArgs e)
        {
            UserListWindow ulw = new UserListWindow(client);
            ulw.ShowDialog();
        }

        private void itemNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow nuw = new NewUserWindow(client);
            nuw.ShowDialog();
        }

        private void itemUpdateUSer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Válasszon ki egy felhasználót a listából a módosításhoz!");
            itemUserList_Click(sender, e);
        }

        private void itemDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Válasszon ki egy felhasználót a listából a törléshez!");
            itemUserList_Click(sender, e);
        }
    }
}