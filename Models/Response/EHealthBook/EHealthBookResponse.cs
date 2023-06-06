using AppDentistry.Models.Response.Clinic;
using AppDentistry.Models.Response.Customer;
using AppDentistry.Models.Response.Doctor;
using AppDentistry.Models.Response.EHealthBookDetail;
using AppDentistry.Models.Response.EHealthBookService;
using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Response.EHealthBook
{
    public class EHealthBookResponse
    {
        public int Id { get; set; }
        public double TotalFee { get; set; }
        public int IdClinic { get; set; }
        public int IdCustomer { get; set; }
        public int? IdDoctorInCharge { get; set; }
        public DateTime CheckUpDate { get; set; }
        public DateTime? ReExaminationDate { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public List<EHealthBookDetailResponse> EHealthBookDetails { get; set; }
        public List<EHealthBookServiceResponse> EHealthBookServices { get; set; }
        public ClinicResponse Clinic { get; set; }
        public CustomerResponse Customer { get; set; }
        public DoctorResponse Doctor { get; set; }
    }
}
