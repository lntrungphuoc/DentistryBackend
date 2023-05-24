using AppDentistry.Models.Response.Clinic;
using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Response.AppointmentSchedule
{
    public class AppointmentScheduleResponse
    {
        public int Id { get; set; }
        public int IdClinic { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public bool IsConfirm { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public ClinicResponse Clinic { get; set; }
    }
}
