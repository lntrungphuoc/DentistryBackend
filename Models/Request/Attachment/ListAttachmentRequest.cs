namespace AppDentistry.Models.Request.Attachment
{
    public class ListAttachmentRequest
    {
        public IFormFile ListURL { get; set; }
        public int IdEHealthBookDetail { get; set; }
    }
}
