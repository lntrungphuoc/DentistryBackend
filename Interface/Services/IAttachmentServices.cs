using AppDentistry.Models.Request.Attachment;
using AppDentistry.Models.Response.Attachment;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IAttachmentServices
    {
        HttpResponse Create(ListAttachmentRequest listRequest);
        HttpResponse UpdateByEHealthBookDetail(List<AttachmentRequest> listAttachment, int idUser, int idEHealthBookDetail);
        List<AttachmentResponse> GetByEHealthBookDetail(int id);
    }
}
