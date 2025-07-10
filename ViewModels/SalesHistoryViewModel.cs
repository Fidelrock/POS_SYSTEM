using POS_SYSTEM.Models;
using POS_SYSTEM.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace POS_SYSTEM.ViewModels
{
    public class SalesHistoryViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService = new DatabaseService();

        public ObservableCollection<Sale> Sales { get; set; } = new ObservableCollection<Sale>();
        public ObservableCollection<SaleItem> SaleItems { get; set; } = new ObservableCollection<SaleItem>();

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); }
        }

        private string _productFilter;
        public string ProductFilter
        {
            get => _productFilter;
            set { _productFilter = value; OnPropertyChanged(); }
        }

        private Sale _selectedSale;
        public Sale SelectedSale
        {
            get => _selectedSale;
            set
            {
                _selectedSale = value;
                OnPropertyChanged();
                LoadSaleItems();
            }
        }

        private decimal _totalSales;
        public decimal TotalSales
        {
            get => _totalSales;
            set { _totalSales = value; OnPropertyChanged(); }
        }

        public ICommand FilterCommand { get; }

        public SalesHistoryViewModel()
        {
            FilterCommand = new RelayCommand(FilterSales);
            StartDate = DateTime.Today.AddDays(-7);
            EndDate = DateTime.Today;
            FilterSales();
        }

        private void FilterSales()
        {
            Sales.Clear();
            var allSales = _databaseService.GetAllSales();
            var filtered = allSales.AsQueryable();
            if (StartDate.HasValue)
                filtered = filtered.Where(s => s.Date >= StartDate.Value);
            if (EndDate.HasValue)
                filtered = filtered.Where(s => s.Date <= EndDate.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(ProductFilter))
                filtered = filtered.Where(s => s.SaleItems.Any(
                    si => (si.Product.Name != null && si.Product.Name.ToLower().Contains(ProductFilter.ToLower()))
                       || (si.Product.Barcode != null && si.Product.Barcode.ToLower().Contains(ProductFilter.ToLower()))
                ));
            foreach (var sale in filtered.OrderByDescending(s => s.Date))
                Sales.Add(sale);
            TotalSales = Sales.Sum(s => s.TotalAmount);
            if (Sales.Count > 0)
                SelectedSale = Sales[0];
            else
                SaleItems.Clear();
        }

        private void LoadSaleItems()
        {
            SaleItems.Clear();
            if (SelectedSale != null)
            {
                foreach (var item in _databaseService.GetSaleItemsBySaleId(SelectedSale.SaleId))
                {
                    
                    SaleItems.Add(item);
                }
            }
        }
    }
}
