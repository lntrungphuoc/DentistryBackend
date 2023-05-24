using AppDentistry.Models.Response.EHealthBook;

namespace AppDentistry.Models.Response.Customer
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
        public string? CCCD { get; set; }
        public string BHYT { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public List<EHealthBookResponse> EHealthBooks { get; set; }
    }
}
