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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_btn(object sender, RoutedEventArgs e)
        {
            string Login = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            var user = new User
            {
                Login = Login,
                Password = password
            };
            foreach (var u in App.Db_user.Users)
            {
                if (u.Login == user.Login && u.Password == user.Password)
                {
                    user.Id = u.Id;
                    enter_acc(user);
                    break;
                }
            }
        }
        private void enter_acc(User user)
        {
            var mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }
    }
}
