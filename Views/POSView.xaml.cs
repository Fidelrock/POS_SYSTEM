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
    /// Interaction logic for POSView.xaml
    /// </summary>
    public partial class POSView : UserControl
    {
        public POSView()
        {
            InitializeComponent();
        }

        private void BarcodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = DataContext as POS_SYSTEM.ViewModels.POSViewModel;
                if (vm != null && vm.ScanBarcodeCommand.CanExecute(null))
                {
                    vm.ScanBarcodeCommand.Execute(null);
                }
            }
        }
    }
}
