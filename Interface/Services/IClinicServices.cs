using AppDentistry.Models.Request.Clinic;
using AppDentistry.Models.Response.Clinic;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IClinicServices
    {
        List<ClinicResponse> GetAll();
        HttpResponse Create(ClinicRequest request);
        HttpResponse Update(ClinicRequest request);
        HttpResponse Delete(int id);
    }
}
