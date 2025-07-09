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
using System.Windows.Navigation;
using System.Windows.Shapes;
using POS_SYSTEM.Models;

namespace POS_SYSTEM.Views
{
    /// <summary>
    /// Interaction logic for ProductDialog.xaml
    /// </summary>
    public partial class ProductDialog : Window
    {
        public Product Product { get; private set; }
        public List<Category> Categories { get; set; }

        public ProductDialog(Product product, List<Category> categories)
        {
            InitializeComponent();
            Categories = categories;
            CategoryBox.ItemsSource = Categories;
            if (product != null)
            {
                // Editing existing product
                Product = new Product
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Barcode = product.Barcode,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Stock = product.Stock
                };
                NameBox.Text = Product.Name;
                BarcodeBox.Text = Product.Barcode;
                PriceBox.Text = Product.Price.ToString();
                StockBox.Text = Product.Stock.ToString();
                CategoryBox.SelectedValue = Product.CategoryId;
            }
            else
            {
                // Adding new product
                Product = new Product();
                CategoryBox.SelectedIndex = 0;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                string.IsNullOrWhiteSpace(BarcodeBox.Text) ||
                CategoryBox.SelectedItem == null ||
                !decimal.TryParse(PriceBox.Text, out decimal price) ||
                !int.TryParse(StockBox.Text, out int stock))
            {
                MessageBox.Show("Please enter valid product details.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Product.Name = NameBox.Text.Trim();
            Product.Barcode = BarcodeBox.Text.Trim();
            Product.CategoryId = (int)CategoryBox.SelectedValue;
            Product.Price = price;
            Product.Stock = stock;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
