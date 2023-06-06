using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Request.EHealthBook
{
    public class EHealthBookRequest
    {
        public int Id { get; set; }
        public double TotalFee { get; set; }
        public int IdClinic { get; set; }
        public int IdCustomer { get; set; }
        public int? IdDoctorInCharge { get; set; }
        public DateTime CheckUpDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReExaminationDate { get; set; }
        public string? Note { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
