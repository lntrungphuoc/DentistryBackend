using AppDentistry.Models.Request.Service;
using AppDentistry.Models.Response.Service;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;
namespace AppDentistry.Interface.Services
{
    public interface IServiceServices
    {
        List<ServiceResponse> GetAll();
        ServiceResponse GetByURL(string url);
        HttpResponse Create(ServiceRequest request);
        HttpResponse Update(ServiceRequest request);
        HttpResponse Delete(int id);
        float CalculateFee(List<ServiceRequest> requests);
    }
}
