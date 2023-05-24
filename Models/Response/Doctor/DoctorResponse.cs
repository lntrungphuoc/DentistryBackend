using AppDentistry.Models.Response.User;
using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Response.Doctor
{
    public class DoctorResponse
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Information { get; set; }
        public bool IsDoctor { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserResponse UserResponse { get; set; }
    }
}
