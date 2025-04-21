namespace EBill.DTO
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public int CategoryId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
