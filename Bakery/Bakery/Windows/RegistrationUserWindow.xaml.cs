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
using Bakery.DB;
using static Bakery.ClassHelper.EFClass;
using Bakery.Windows;
using System.Runtime.Serialization.Formatters;

namespace Bakery.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationUserWindow.xaml
    /// </summary>
    public partial class RegistrationUserWindow : Window
    {
        public RegistrationUserWindow()
        {
            InitializeComponent();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            // валидация 
            if (string.IsNullOrWhiteSpace(TbLogin.Text))
            {
                MessageBox.Show("Пустой логин");
                return;
            }
            if (string.IsNullOrWhiteSpace(Pass.Password))
            {
                MessageBox.Show("Пустой пароль");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                MessageBox.Show("Пустой телефон");
                return;
            }
            

            Context.Login.Add(new DB.Login
            {
               
                Password = Pass.Password,
                login1 = TbLogin.Text,
                phone = TbPhone.Text,
                roleid = 1
            }) ;
            
            Context.SaveChanges();
            ProductListWindow productlistWindow = new ProductListWindow();
            productlistWindow.Show();
            this.Close();
        }       
    }
}
