namespace POS_SYSTEM.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public virtual Category Category { get; set; }
    }
}