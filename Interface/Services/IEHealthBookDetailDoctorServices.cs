using AppDentistry.Models.Request.EHealthBookDetailDoctor;
using AppDentistry.Models.Response.EHealthBookDetailDoctor;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IEHealthBookDetailDoctorServices
    {
        List<EHealthBookDetailDoctorResponse> GetByEHealthBookDetailId(int id);
        List<EHealthBookDetailDoctorResponse> GetEHealthBookDetailByDoctorId(int idUser);
        HttpResponse Create(EHealthBookDetailDoctorRequest request);
        HttpResponse Update(EHealthBookDetailDoctorRequest request);
        HttpResponse Delete(int id);
        HttpResponse DeleteRange(int idEHealthBookDetail);
    }
}
