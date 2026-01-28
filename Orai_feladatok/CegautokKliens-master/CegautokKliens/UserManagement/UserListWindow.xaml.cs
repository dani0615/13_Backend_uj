using CegautokKliens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CegautokKliens.UserManagement
{
    /// <summary>
    /// Interaction logic for UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window
    {
        private static HttpClient _client;
        List<User> users = new List<User>();

        public UserListWindow(HttpClient client)
        {
            _client = client;
            InitializeComponent();
        }

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string url = $"{_client.BaseAddress}User/Users";
            var response = await _client.GetFromJsonAsync<List<User>>(url);
            if (response != null) {
                users = response;
                dgrUsers.ItemsSource = users;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
