using POS_SYSTEM.Models;
using POS_SYSTEM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using System;
using System.Linq;

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

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CartItem> Cart { get; set; } = new ObservableCollection<CartItem>();

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
            ScanBarcodeCommand = new RelayCommand(ScanBarcode);
            RemoveFromCartCommand = new RelayCommand<CartItem>(RemoveFromCart);
            CheckoutCommand = new RelayCommand(Checkout);
        }

        private void ScanBarcode()
        {
            if (string.IsNullOrWhiteSpace(BarcodeInput))
                return;

            var product = _databaseService.GetProductByBarcode(BarcodeInput);
            if (product != null)
            {
                if (product.Stock <= 0)
                {
                    Message = $"'{product.Name}' is out of stock.";
                }
                else
                {
                    var existingItem = Cart.FirstOrDefault(ci => ci.Product.ProductId == product.ProductId);
                    if (existingItem != null)
                    {
                        if (existingItem.Quantity < product.Stock)
                        {
                            existingItem.Quantity++;
                            Message = string.Empty;
                        }
                        else
                        {
                            Message = $"Cannot add more '{product.Name}'. Only {product.Stock} in stock.";
                        }
                    }
                    else
                    {
                        Cart.Add(new CartItem { Product = product, Quantity = 1 });
                        Message = string.Empty;
                    }
                    CalculateTotal();
                }
            }
            else
            {
                Message = $"Product with barcode '{BarcodeInput}' not found.";
            }
            BarcodeInput = string.Empty;
        }

        private void RemoveFromCart(CartItem cartItem)
        {
            if (cartItem == null) return;
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                Message = string.Empty;
            }
            else
            {
                Cart.Remove(cartItem);
                Message = string.Empty;
            }
            CalculateTotal();
        }

        private void Checkout()
        {
            if (Cart.Count == 0)
                return;

            // Validate all cart items before checkout
            foreach (var cartItem in Cart)
            {
                if (cartItem.Quantity > cartItem.Product.Stock)
                {
                    Message = $"Only {cartItem.Product.Stock} of '{cartItem.Product.Name}' in stock. Please adjust quantity.";
                    return;
                }
            }

            var sale = new Sale
            {
                Date = DateTime.Now,
                TotalAmount = Total
            };

            var saleItems = new List<SaleItem>();
            foreach (var cartItem in Cart)
            {
                // Update stock
                cartItem.Product.Stock -= cartItem.Quantity;
                _databaseService.UpdateProduct(cartItem.Product);

                saleItems.Add(new SaleItem
                {
                    ProductId = cartItem.Product.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price
                });
            }

            _databaseService.AddSale(sale, saleItems);

            Cart.Clear();
            Total = 0;
            Message = "Checkout successful!";
        }

        private void CalculateTotal()
        {
            Total = Cart.Sum(item => item.Subtotal);
        }

        public void ValidateCartItemQuantity(CartItem cartItem)
        {
            if (cartItem.Quantity < 1)
            {
                cartItem.Quantity = 1;
                Message = "Quantity cannot be less than 1.";
            }
            else if (cartItem.Quantity > cartItem.Product.Stock)
            {
                cartItem.Quantity = cartItem.Product.Stock;
                Message = $"Only {cartItem.Product.Stock} '{cartItem.Product.Name}' in stock.";
            }
            else
            {
                Message = string.Empty;
            }
            CalculateTotal();
        }
    }
} 