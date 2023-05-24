using AppDentistry.Models.Response.Attachment;
using AppDentistry.Models.Response.Clinic;
using AppDentistry.Models.Response.Doctor;
using AppDentistry.Models.Response.EHealthBook;
using AppDentistry.Models.Response.EHealthBookDetailDoctor;
using AppDentistry.Models.Response.EHealthBookDetailService;
using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Response.EHealthBookDetail
{
    public class EHealthBookDetailResponse
    {
        public int Id { get; set; }
        [Required]
        public string Diagnose { get; set; }
        public string Medicine { get; set; }
        public int IdEHealthBook { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public List<EHealthBookDetailDoctorResponse> EHealthBookDetailDoctor { get; set; }
        public List<AttachmentResponse> Attachments { get; set; }
        public List<EHealthBookDetailServiceResponse> EHealthBookDetailServices { get; set; }
        public EHealthBookResponse EHealthBook { get; set; }
    }
}
