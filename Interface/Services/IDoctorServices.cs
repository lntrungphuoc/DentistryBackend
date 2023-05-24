using AppDentistry.Models.Request.Doctor;
using AppDentistry.Models.Response.Doctor;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IDoctorServices
    {
        List<DoctorResponse> GetAll();
        DoctorResponse GetById(int id);
        HttpResponse Create(DoctorRequest request);
        HttpResponse Update(DoctorRequest request);
        HttpResponse Delete(int id);
    }
}
