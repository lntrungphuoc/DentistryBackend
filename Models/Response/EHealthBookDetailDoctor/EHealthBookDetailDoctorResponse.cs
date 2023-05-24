using AppDentistry.Models.Response.Doctor;
using AppDentistry.Models.Response.EHealthBookDetail;

namespace AppDentistry.Models.Response.EHealthBookDetailDoctor
{
    public class EHealthBookDetailDoctorResponse
    {
        public int Id { get; set; }
        public int IdDoctor { get; set; }
        public int IdEHealthBookDetail { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DoctorResponse Doctor { get; set; }
        public EHealthBookDetailResponse EHealthBookDetail { get; set; }
    }
}
