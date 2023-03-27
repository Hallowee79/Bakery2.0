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

using Bakery.DB;
using Microsoft.Win32;
using System.IO;

namespace Bakery.Windows
{
    public partial class AddEditProductWindow : Window
    {

        private string pathPhoto = null;

        private bool isEdit = false;

        private Product editProduct;


        public AddEditProductWindow()
        {
            InitializeComponent();

            CMBTypeProduct.ItemsSource = Context.ProductType.ToList();
            CMBTypeProduct.SelectedIndex = 0;
            CMBTypeProduct.DisplayMemberPath = "TypeName";
            tb1.Text = "Добавление товара";
            BtnAddEdit.Content = "Добавить";
        }

        public AddEditProductWindow(Product product)
        {
            InitializeComponent();

            editProduct = product;

            CMBTypeProduct.ItemsSource = Context.ProductType.ToList();
            CMBTypeProduct.SelectedIndex = 0;
            CMBTypeProduct.DisplayMemberPath = "TypeName";

            TbNameProduct.Text = product.Title.ToString();
            TbDisc.Text = product.Description.ToString();
            TbCost.Text = product.Cost.ToString();
            check.IsChecked = product.Active;
            CMBTypeProduct.SelectedItem = Context.ProductType.Where(i => i.id == product.ProductTypeid).FirstOrDefault();
            if (product.ImagePath != null)
            {

                using (MemoryStream stream = new MemoryStream(product.ImagePath))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    ImgProduct.Source = bitmapImage;

                }
            }
            isEdit = true;



        }

        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {

            if (isEdit)
            {


                editProduct.Title = TbNameProduct.Text;
                editProduct.Description = TbDisc.Text;
                editProduct.Cost = Convert.ToDecimal(TbCost.Text);
                editProduct.ProductTypeid = (CMBTypeProduct.SelectedItem as ProductType).id;
                editProduct.Active = check.IsChecked;
                if (pathPhoto != null)
                {
                    editProduct.ImagePath = File.ReadAllBytes(pathPhoto);
                }
                Context.SaveChanges();

            }
            else
            {
                Product product = new Product();
                product.Title = TbNameProduct.Text;
                product.Description = TbDisc.Text;
                product.Cost = Convert.ToDecimal(TbCost.Text);
                product.ProductTypeid = (CMBTypeProduct.SelectedItem as ProductType).id;
                product.Active = check.IsChecked;
                if (pathPhoto != null)
                {
                    product.ImagePath = File.ReadAllBytes(pathPhoto);
                }

                Context.Product.Add(product);

                Context.SaveChanges();
            }
            ProductListWindow productlistWindow = new ProductListWindow();

            this.Close();

        }

        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathPhoto = openFileDialog.FileName;
            }
        }


    }
}
