using AppDentistry.Models.Request.EHealthBookDetail;
using AppDentistry.Models.Response.EHealthBookDetail;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IEHealthBookDetailServices
    {
        List<EHealthBookDetailResponse> GetByIdHealthBook(int id);
        HttpResponse Create(EHealthBookDetailRequest request);
        HttpResponse Update(EHealthBookDetailRequest request);
        HttpResponse Delete(int id);
    }
}
