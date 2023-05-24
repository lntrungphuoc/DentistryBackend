namespace AppDentistry.Models.Request.EHealthBookService
{
    public class EHealthBookServiceRequest
    {
        public int Id { get; set; }
        public int IdEHealthBook { get; set; }
        public int IdService { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
