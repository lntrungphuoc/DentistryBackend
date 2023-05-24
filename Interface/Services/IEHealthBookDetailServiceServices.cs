using AppDentistry.Models.Request.EHealthBookDetailService;
using AppDentistry.Models.Response.EHealthBookDetailService;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IEHealthBookDetailServiceServices
    {
        List<EHealthBookDetailServiceResponse> GetByEHealthBookDetailId(int id);
        HttpResponse Create(EHealthBookDetailServiceRequest request);
        HttpResponse Update(EHealthBookDetailServiceRequest request);
        HttpResponse Delete(int id);
        HttpResponse DeleteRange(int idEHealthBookDetail);
    }
}
