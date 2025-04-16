using EBill.Models.Entities.Common;
using EBill.Models.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBill.Models.Entities
{
    public class Product : Base
    {
        public string Name { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
    }
}
