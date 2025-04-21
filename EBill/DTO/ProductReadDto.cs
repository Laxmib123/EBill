namespace EBill.DTO
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public string Category {  get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
