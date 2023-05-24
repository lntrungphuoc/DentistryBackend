namespace AppDentistry.Models.Request.Attachment
{
    public class AttachmentRequest
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string FileName { get; set; }
        public int IdEHealthBookDetail { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
