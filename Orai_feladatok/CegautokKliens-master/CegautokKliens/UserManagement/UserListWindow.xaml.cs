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
            try
            {
                var response = await _client.GetFromJsonAsync<List<User>>("User/Users");
                if (response != null)
                {
                    users = response;
                    dgrUsers.ItemsSource = null;
                    dgrUsers.ItemsSource = users;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a betöltés során: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgrUsers.SelectedItem is User selectedUser)
            {
                UpdateUserWindow uw = new UpdateUserWindow(_client, selectedUser);
                uw.ShowDialog();
                btnLoad_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy felhasználót!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrUsers.SelectedItem is User selectedUser)
            {
                DeleteUserWindow dw = new DeleteUserWindow(_client, selectedUser);
                dw.ShowDialog();
                btnLoad_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy felhasználót!");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
