using EBill.Models.Entities.Common;

namespace EBill.Models.Entities
{
    public class Supplier : Base
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
    }
}
