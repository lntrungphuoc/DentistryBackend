namespace AppDentistry.Models.Request.Attachment
{
    public class UpdateAttachmentRequest
    {
        public List<AttachmentRequest> listAttachment { get; set; }
        public int idEHealthBookDetail { get; set; }
    }
}
