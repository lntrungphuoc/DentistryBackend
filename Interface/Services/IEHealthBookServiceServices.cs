using AppDentistry.Models.Request.EHealthBookService;
using AppDentistry.Models.Response.EHealthBookService;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IEHealthBookServiceServices
    {
        List<EHealthBookServiceResponse> GetByEHealthBookId(int id);
        HttpResponse Create(EHealthBookServiceRequest request);
        HttpResponse Update(EHealthBookServiceRequest request);
        HttpResponse Delete(int id);
        HttpResponse DeleteRange(int idEHealthBook);
    }
}
