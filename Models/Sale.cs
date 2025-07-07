using System;
using System.Collections.Generic;

namespace POS_SYSTEM.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }
}