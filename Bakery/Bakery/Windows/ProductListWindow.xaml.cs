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
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По имени (по возрастанию)",
            "По имени (по убыванию)",
            "По типу (по возрастанию)",
            "По типу (по убыванию)",
            "По цене (по возрастанию)",
            "По цене (по убыванию)",

        };
        List<string> listFilter = new List<string>()
        {
            "По умолчанию",
            "Напитки",
            "Выпечка",
            "Торты"
        };
        public ProductListWindow()
        {
            InitializeComponent();
            CmbSort.ItemsSource = listSort;
            CmbSort.SelectedIndex = 0;
            CmbFilter.ItemsSource = listFilter;
            CmbFilter.SelectedIndex = 0;
            GetListProduct();
            if (userlog.UserDataClass.user.roleid == 3)
            {
                BtnAddProduct.Visibility = Visibility.Hidden;
            }
        }
        private void GetListProduct()
        {
            List<Product> products = new List<Product>();
            products = Context.Product.ToList();

            // поиск, сортировка, фильтрация

            bool a = (bool)allcheck.IsChecked;
            if (a == false)
            {
                string x = "true";
                products = products.Where(i => i.Active.ToString().ToLower().Contains(x.ToLower())).ToList();
            }


            
            // поиск
            products = products.Where(i => i.Title.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            // сортировка
            var selectedIndexCmb = CmbSort.SelectedIndex;
            switch (selectedIndexCmb)
            {
                case 0:
                    products = products.OrderBy(i => i.ProductTypeid).ToList();
                    break;

                case 1:
                    products = products.OrderBy(i => i.Title.ToLower()).ToList();
                    break;

                case 2:
                    products = products.OrderByDescending(i => i.Title.ToLower()).ToList();
                    break;
                case 3:
                    products = products.OrderBy(i => i.ProductType.TypeName.ToLower()).ToList();
                    break;
                case 4:
                    products = products.OrderByDescending(i => i.ProductType.TypeName.ToLower()).ToList();
                    break;
                case 5:
                    products = products.OrderBy(i => i.Cost).ToList();
                    break;
                case 6:
                    products = products.OrderByDescending(i => i.Cost).ToList();
                    break;
                default:
                    break;

            }
            // фильтрация
            var selectedIndexCmb2 = CmbFilter.SelectedIndex;
            switch (selectedIndexCmb2)
            {
                case 1:
                    products = products.Where(i => i.ProductType.TypeName.ToLower().Contains("Напитки".ToLower())).ToList();
                    break;
                case 2:
                    products = products.Where(i => i.ProductType.TypeName.ToLower().Contains("Выпечка".ToLower())).ToList();
                    break;
                case 3:
                    products = products.Where(i => i.ProductType.TypeName.ToLower().Contains("Торты".ToLower())).ToList();
                    break;
                default:
                    break;

            }



            // вывод итгового списка
            LvProduct.ItemsSource = products;

        }

        private void Allcheck_Checked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddEditProductWindow addEditProductWindow = new AddEditProductWindow();
            addEditProductWindow.ShowDialog();

        }
        private void BtnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (userlog.UserDataClass.user.roleid == 3)
            {

            }
            else
            {
                var button = sender as Button;
                if (button == null)
                {
                    return;
                }
                var product = button.DataContext as Product;

                AddEditProductWindow editProductWindow = new AddEditProductWindow(product);
                editProductWindow.ShowDialog();

                GetListProduct();
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListProduct();
        }
        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListProduct();
        }

        private void Allcheck_UnChecked(object sender, RoutedEventArgs e)
        {
            GetListProduct();
        }

        private void allcheck_Checked_2(object sender, RoutedEventArgs e)
        {
            GetListProduct();
        }
        public int m = 0;

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListProduct();

        }
        //корзина
        private void BtnAddToCartProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var product = button.DataContext as Product;
            bool m = (bool)product.Active;
            if (m == true)
            {
                CartProductClass.products.Add(product);
                product.Quantity = product.Quantity - 1;
                int z = Convert.ToInt32(product.Quantity);
                if (z == 0)
                {
                    product.Active = false;
                }
            }
            GetListProduct();
        }

        private void ImgCart_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CartWindow cartProductWindow = new CartWindow();
            cartProductWindow.Show();
            this.Close();

        }

        private void Btnexit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}