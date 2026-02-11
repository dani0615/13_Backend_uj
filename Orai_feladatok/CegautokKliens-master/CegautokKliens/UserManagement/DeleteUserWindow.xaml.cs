using CegautokKliens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CegautokKliens.UserManagement
{
    public partial class DeleteUserWindow : Window
    {
        private static HttpClient _client;
        private User _user;

        public DeleteUserWindow(HttpClient client, User user)
        {
            _client = client;
            _user = user;
            InitializeComponent();
            tbUserInfo.Text = $"Name: {_user.Name}\nLogin: {_user.LoginName}\nEmail: {_user.Email}";
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var response = await _client.DeleteAsync($"User/User/{_user.Id}");
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Sikeres törlés!");
                Close();
            }
            else
            {
                MessageBox.Show("Hiba a törlés során: " + response.ReasonPhrase);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
