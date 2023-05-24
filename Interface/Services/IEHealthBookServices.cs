using AppDentistry.Models.Request.EHealthBook;
using AppDentistry.Models.Response.EHealthBook;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IEHealthBookServices
    {
        List<EHealthBookResponse> GetAll();
        List<EHealthBookResponse> GetById(int id);
        List<EHealthBookResponse> GetByIdUser(int id);
        EHealthBookResponse GetNewest();
        HttpResponse Create(EHealthBookRequest request);
        HttpResponse Update(EHealthBookRequest request);
        HttpResponse Delete(int id);
    }
}
