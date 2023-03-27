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

using static Bakery.ClassHelper.EFClass;
using Bakery.Windows;

namespace Bakery.Windows
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var userAuth = Context.Login.ToList()
                .Where(i => i.login1 == TbLogin.Text &&
                i.Password == Pass.Password)
                .FirstOrDefault();
                ClassHelper.userlog.UserDataClass.user = userAuth;
            if (userAuth != null)
            {  
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationUserWindow registrationUserWindow = new RegistrationUserWindow();
            registrationUserWindow.Show();
            this.Close();
        }
    }
}


