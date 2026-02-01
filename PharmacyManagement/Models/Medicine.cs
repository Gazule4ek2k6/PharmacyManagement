using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public string? Manufacturer {  get; set; }
        public string? DosageForm { get; set; }
        public bool? PrescriptionRequired { get; set; }
        public string? Price { get; set; }
        public string? Quantity { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public string? Photo {  get; set; }

        public ICollection<Sale>? Sales { get; set; }
    }
}
