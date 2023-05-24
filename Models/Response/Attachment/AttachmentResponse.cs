using AppDentistry.Models.Response.EHealthBookDetail;

namespace AppDentistry.Models.Response.Attachment
{
    public class AttachmentResponse
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int IdEHealthBookDetail { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public EHealthBookDetailResponse EHealthBookDetail { get; set; }
    }
}
