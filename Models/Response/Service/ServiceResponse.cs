using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Response.Service
{
    public class ServiceResponse
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Information { get; set; }
        [Required]
        public float Fee { get; set; }
        public string URL { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
