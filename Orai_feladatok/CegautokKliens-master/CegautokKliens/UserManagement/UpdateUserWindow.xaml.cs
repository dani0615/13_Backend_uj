using CegautokKliens.Models;
using CegautokKliens.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CegautokKliens.UserManagement
{
    public partial class UpdateUserWindow : Window
    {
        private static HttpClient _client;
        private User _user;
        private static List<int> permissions = new List<int>() { 1, 2, 3, 10, 6 };

        public UpdateUserWindow(HttpClient client, User user)
        {
            _client = client;
            _user = user;
            InitializeComponent();
            cbxPermission.ItemsSource = permissions;
            LoadUserData();
        }

        private void LoadUserData()
        {
            tbxName.Text = _user.Name;
            tbxLoginName.Text = _user.LoginName;
            tbxEmail.Text = _user.Email;
            tbxAddress.Text = _user.Address;
            tbxPhone.Text = _user.Phone;
            tbxImage.Text = _user.Image;
            cbxPermission.SelectedItem = _user.Permission;
            chbActive.IsChecked = _user.Active;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(pbxPassword.Password))
            {
                if (pbxPassword.Password != pbxConfirmPassword.Password)
                {
                    MessageBox.Show("A jelszavak nem egyeznek!");
                    return;
                }
                string salt = new LoginServices().GenerateSalt();
                string simpleHash = new LoginServices().CreateSHA256(pbxPassword.Password + salt);
                _user.Salt = salt;
                _user.Hash = new LoginServices().CreateSHA256(simpleHash);
            }

            _user.Name = tbxName.Text;
            _user.LoginName = tbxLoginName.Text;
            _user.Address = tbxAddress.Text;
            _user.Email = tbxEmail.Text;
            _user.Phone = tbxPhone.Text;
            _user.Image = tbxImage.Text;
            _user.Permission = (int)cbxPermission.SelectedValue;
            _user.Active = chbActive.IsChecked ?? false;

            var response = await _client.PutAsJsonAsync($"User/User/{_user.Id}", _user);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Sikeres frissítés!");
                Close();
            }
            else
            {
                MessageBox.Show("Hiba a frissítés során: " + response.ReasonPhrase);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
