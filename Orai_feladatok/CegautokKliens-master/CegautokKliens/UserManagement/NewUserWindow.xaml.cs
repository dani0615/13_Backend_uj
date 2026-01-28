using CegautokKliens.Models;
using CegautokKliens.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        private static HttpClient _client;
        private static List<int> permissions = new List<int>() { 1,2, 3, 10,6 };

        public NewUserWindow(HttpClient client)
        {
            _client = client;
            InitializeComponent();
            cbxPermission.ItemsSource = permissions;
            cbxPermission.SelectedIndex = 0;
        }

        private void tbxSave_Click(object sender, RoutedEventArgs e)
        {
            string salt = new LoginServices().GenerateSalt();
            string simpleHash = new LoginServices().CreateSHA256(pbxPassword.Password + salt);
            User newUser = new User()
            {
                Name = tbxName.Text,
                LoginName = tbxLoginName.Text,
                Address = tbxAdress.Text,
                Email = tbxEmail.Text,
                Salt = salt,
                Hash = new LoginServices().CreateSHA256(simpleHash),
                Phone = tbxPhone.Text,
                Permission = (int)cbxPermission.SelectedValue,
                Active = chbActive.IsChecked ?? false

            };
            string toSend = JsonSerializer.Serialize(newUser, JsonSerializerOptions.Default);
            var content = new StringContent(toSend, Encoding.UTF8, "application/json");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
