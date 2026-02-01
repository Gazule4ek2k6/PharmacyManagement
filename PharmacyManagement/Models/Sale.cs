using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime? SaleDateTime { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int MedicineId { get; set; }
        public Medicine? Medicine { get; set; }
        public string? Quantity { get; set; }
        public string? Amount { get; set; }
    }
}
