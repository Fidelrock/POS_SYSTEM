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

namespace POS_SYSTEM.Views
{
    /// <summary>
    /// Interaction logic for UpdateStockDialog.xaml
    /// </summary>
    public partial class UpdateStockDialog : Window
    {
        public int NewStock { get; private set; }

        public UpdateStockDialog(int currentStock)
        {
            InitializeComponent();
            CurrentStockText.Text = currentStock.ToString();
            NewStockBox.Text = currentStock.ToString();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(NewStockBox.Text, out int newStock) || newStock < 0)
            {
                MessageBox.Show("Please enter a valid non-negative integer for stock.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            NewStock = newStock;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
