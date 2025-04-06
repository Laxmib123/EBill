namespace EBill.Models.Entities.Common
{
    public class Base
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool status { get; set; }
    }

}
