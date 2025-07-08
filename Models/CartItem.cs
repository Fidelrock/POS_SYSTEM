using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace POS_SYSTEM.Models
{
    public class CartItem : INotifyPropertyChanged
    {
        private int _quantity;
        public Product Product { get; set; }
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); OnPropertyChanged(nameof(Subtotal)); }
        }
        public decimal Subtotal => Product.Price * Quantity;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 