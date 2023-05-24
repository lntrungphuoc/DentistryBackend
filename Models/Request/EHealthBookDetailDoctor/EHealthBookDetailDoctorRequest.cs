namespace AppDentistry.Models.Request.EHealthBookDetailDoctor
{
    public class EHealthBookDetailDoctorRequest
    {
        public int Id { get; set; }
        public int IdDoctor { get; set; }
        public int IdEHealthBookDetail { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
