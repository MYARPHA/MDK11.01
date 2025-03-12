using LabWork20.Data;
using LabWork20.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LabWork20
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private readonly UserService _service = new();
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var user = await _service.GetUserAsync(LoginTextBox.Text, PasswordTextBox.Text);
            if (user != null)
            {
                MessageBox.Show("Вы вошли!");
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
