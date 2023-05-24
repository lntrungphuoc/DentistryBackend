using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Request.Doctor
{
    public class DoctorRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public bool IsDoctor { get; set; }
        public IFormFile? AvatarUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
