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
using TeamProject.Entities;

namespace TeamProject
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private bool _isPasswordVisible = false;
        public Registration()
        {
            InitializeComponent();
            
        }

        private void Register_Btn(object sender, RoutedEventArgs e)
        {
            string Login = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            bool is_registered = false;
            var user = new User
            {
                Login = Login,
                Password = password
            };
            foreach (var u in App.Db_user.Users) //прохожусь по базі в пошуках однакових імені та пароля
            {
                    if (u.Login == user.Login && u.Password == user.Password)
                    {
                        MessageBox.Show("this user already exist!");
                        is_registered = true;
                        break;
                    }
            }
            if(is_registered == false)
            {
                App.Db_user.Add(user);
                App.Db_user.SaveChanges();
                var mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }

        }
        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;

            if (_isPasswordVisible)
            {
                VisiblePasswordBox.Text = PasswordBox.Password;
                VisiblePasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordBox.Password = VisiblePasswordBox.Text;
                PasswordBox.Visibility = Visibility.Visible;
                VisiblePasswordBox.Visibility = Visibility.Collapsed;
            }
        }

        private void Already_Logined(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            this.Close();
        }
    }
}
