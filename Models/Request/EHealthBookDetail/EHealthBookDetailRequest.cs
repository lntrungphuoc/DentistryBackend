namespace AppDentistry.Models.Request.EHealthBookDetail
{
    public class EHealthBookDetailRequest
    {
        public int Id { get; set; }
        public string Diagnose { get; set; }
        public string Medicine { get; set; }
        public int IdEHealthBook { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
