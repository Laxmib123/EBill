namespace EBill.Models.ViewModel
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int SellingPrice {  get; set; }
        public int PurchasePrice {  get; set; }
        public int CategoryId { get; set; }
    }
}
