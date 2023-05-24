using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Request.AppointmentSchedule
{
    public class AppointmentScheduleRequest
    {
        public int Id { get; set; }
        public int IdClinic { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public bool IsConfirm { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
