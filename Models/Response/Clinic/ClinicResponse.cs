using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Response.Clinic
{
    public class ClinicResponse
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Address { get; set; }
        [Required, StringLength(15)]
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
