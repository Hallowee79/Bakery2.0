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
using Bakery.ClassHelper;
using static Bakery.ClassHelper.EFClass;
using Bakery.Windows;
using Bakery.DB;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace Bakery.Windows
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();

            LvCartProduct.ItemsSource = ClassHelper.CartProductClass.products;
            foreach (Product product in ClassHelper.CartProductClass.products)
            {
                tballcost.Text = Convert.ToString(Convert.ToDouble(tballcost.Text) + Convert.ToDouble(product.Cost.ToString()));
            }
        }
        private void BtnDelToCartProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var product = button.DataContext as Product;
            if (product != null)
            {
                ClassHelper.CartProductClass.products.Remove(product);
                LvCartProduct.ItemsSource = ClassHelper.CartProductClass.products;
            }
            LvCartProduct.Items.Refresh();
            tballcost.Text = Convert.ToString(Convert.ToDouble(tballcost.Text) - Convert.ToDouble(product.Cost.ToString())); ;
        }

        private void BtnBuyProduct_Click(object sender, RoutedEventArgs e)
        {
            foreach (Product product in ClassHelper.CartProductClass.products)
            {
                product.Quantity = product.Quantity - 1;
                int z = Convert.ToInt32(product.Quantity);
                if (z == 0)
                {
                    product.Active = false;
                }
                Context.SaveChanges();

            }
            CartProductClass.products.Clear();
            LvCartProduct.Items.Refresh();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.Show();
            this.Close();
        }
    }
}
