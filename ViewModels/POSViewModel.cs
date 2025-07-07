using POS_SYSTEM.Models;
using POS_SYSTEM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using System;

namespace POS_SYSTEM.ViewModels
{
    public class POSViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService = new DatabaseService();

        private string _barcodeInput;
        public string BarcodeInput
        {
            get => _barcodeInput;
            set { _barcodeInput = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Product> Cart { get; set; } = new ObservableCollection<Product>();

        private decimal _total;
        public decimal Total
        {
            get => _total;
            set { _total = value; OnPropertyChanged(); }
        }

        public ICommand ScanBarcodeCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand CheckoutCommand { get; }

        public POSViewModel()
        {
            // Initialize commands (implementations to be added)
            ScanBarcodeCommand = new RelayCommand(ScanBarcode);
            RemoveFromCartCommand = new RelayCommand<Product>(RemoveFromCart);
            CheckoutCommand = new RelayCommand(Checkout);
        }

        private void ScanBarcode()
        {
            // Lookup product by barcode and add to cart
            var product = _databaseService.GetProductByBarcode(BarcodeInput);
            if (product != null && product.Stock > 0)
            {
                Cart.Add(product);
                CalculateTotal();
            }
            // Optionally handle not found or out of stock
        }

        private void RemoveFromCart(Product product)
        {
            if (Cart.Contains(product))
            {
                Cart.Remove(product);
                CalculateTotal();
            }
        }

        private void Checkout()
        {
            if (Cart.Count == 0)
                return;

            // Create Sale
            var sale = new Sale
            {
                Date = DateTime.Now,
                TotalAmount = Total
            };

            // Create SaleItems (each product in cart is quantity 1 for now)
            var saleItems = new List<SaleItem>();
            foreach (var product in Cart)
            {
                // Update stock
                product.Stock -= 1;
                _databaseService.UpdateProduct(product);

                saleItems.Add(new SaleItem
                {
                    ProductId = product.ProductId,
                    Quantity = 1,
                    UnitPrice = product.Price
                });
            }

            // Save Sale and SaleItems
            _databaseService.AddSale(sale, saleItems);

            // Clear cart and total
            Cart.Clear();
            Total = 0;
        }

        private void CalculateTotal()
        {
            Total = 0;
            foreach (var item in Cart)
            {
                Total += item.Price;
            }
        }
    }
} 