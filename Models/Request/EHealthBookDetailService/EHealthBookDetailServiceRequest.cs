namespace AppDentistry.Models.Request.EHealthBookDetailService
{
    public class EHealthBookDetailServiceRequest
    {
        public int Id { get; set; }
        public int IdEHealthBookDetail { get; set; }
        public int IdService { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
