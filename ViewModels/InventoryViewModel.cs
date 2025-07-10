using POS_SYSTEM.Models;
using POS_SYSTEM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using POS_SYSTEM.Data;
using System.Linq;
using POS_SYSTEM.Views;
using System.Windows;
using System.Collections.Generic;

namespace POS_SYSTEM.ViewModels
{
    public class InventoryViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService = new DatabaseService();

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        private string _messageType;
        public string MessageType
        {
            get => _messageType;
            set { _messageType = value; OnPropertyChanged(); }
        }

        public int LowStockThreshold { get; } = 5;

        public IEnumerable<Product> LowStockProducts => Products.Where(p => p.Stock > 0 && p.Stock <= LowStockThreshold);

        public bool HasLowStockProducts => LowStockProducts.Any();

        private string _lowStockAlert;
        public string LowStockAlert
        {
            get => _lowStockAlert;
            set { _lowStockAlert = value; OnPropertyChanged(); }
        }

        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand UpdateStockCommand { get; }
        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand RefreshCommand { get; }

        public InventoryViewModel()
        {
            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            UpdateStockCommand = new RelayCommand(UpdateStock);
            AddCategoryCommand = new RelayCommand(AddCategory);
            EditCategoryCommand = new RelayCommand(EditCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);
            RefreshCommand = new RelayCommand(LoadData);
            LoadData();
        }

        public void LoadData()
        {
            Products.Clear();
            foreach (var p in _databaseService.GetAllProducts())
                Products.Add(p);
            Categories.Clear();
            foreach (var c in _databaseService.GetAllCategories())
                Categories.Add(c);
            // Low stock alert logic
            if (HasLowStockProducts)
            {
                LowStockAlert = $"Warning: {LowStockProducts.Count()} product(s) are low on stock (≤ {LowStockThreshold}).";
            }
            else
            {
                LowStockAlert = string.Empty;
            }
        }

        private void AddProduct()
        {
            var dialog = new ProductDialog(null, Categories.ToList())
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (dialog.ShowDialog() == true)
            {
                _databaseService.AddProduct(dialog.Product);
                LoadData();
                Message = "Product added.";
                MessageType = "success";
            }
        }

        private void EditProduct()
        {
            if (SelectedProduct == null) { Message = "Select a product to edit."; MessageType = "warning"; return; }
            var dialog = new ProductDialog(SelectedProduct, Categories.ToList())
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (dialog.ShowDialog() == true)
            {
                _databaseService.UpdateProduct(dialog.Product);
                LoadData();
                Message = "Product updated.";
                MessageType = "success";
            }
        }

        private void DeleteProduct()
        {
            if (SelectedProduct == null) { Message = "Select a product to delete."; MessageType = "warning"; return; }
            _databaseService.DeleteProduct(SelectedProduct.ProductId);
            Products.Remove(SelectedProduct);
            Message = "Product deleted.";
            MessageType = "success";
        }

        private void UpdateStock()
        {
            if (SelectedProduct == null) { Message = "Select a product to update stock."; MessageType = "warning"; return; }
            var dialog = new Views.UpdateStockDialog(SelectedProduct.Stock)
            {
                Owner = System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault(w => w.IsActive)
            };
            if (dialog.ShowDialog() == true)
            {
                SelectedProduct.Stock = dialog.NewStock;
                _databaseService.UpdateProduct(SelectedProduct);
                LoadData();
                Message = "Stock updated.";
                MessageType = "success";
            }
        }

        private void AddCategory()
        {
            var dialog = new CategoryDialog(null)
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (dialog.ShowDialog() == true)
            {
                _databaseService.AddCategory(dialog.Category);
                LoadData();
                Message = "Category added.";
                MessageType = "success";
            }
        }

        private void EditCategory()
        {
            if (SelectedCategory == null) { Message = "Select a category to edit."; MessageType = "warning"; return; }
            var dialog = new CategoryDialog(SelectedCategory)
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };
            if (dialog.ShowDialog() == true)
            {
                _databaseService.UpdateCategory(dialog.Category);
                LoadData();
                Message = "Category updated.";
                MessageType = "success";
            }
        }

        private void DeleteCategory()
        {
            if (SelectedCategory == null) { Message = "Select a category to delete."; MessageType = "warning"; return; }
            _databaseService.DeleteCategory(SelectedCategory.CategoryId);
            Categories.Remove(SelectedCategory);
            Message = "Category deleted.";
            MessageType = "success";
        }
    }
}
