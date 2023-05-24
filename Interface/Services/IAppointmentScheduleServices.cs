using AppDentistry.Models.Response.AppointmentSchedule;
using AppDentistry.Models.Request.AppointmentSchedule;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IAppointmentScheduleServices
    {
        List<AppointmentScheduleResponse> GetAll();
        AppointmentScheduleResponse GetById(int id);
        List<AppointmentScheduleResponse> GetConfirmedAppointment();
        List<AppointmentScheduleResponse> GetHaveNotConfirmedAppointment();
        HttpResponse ConfirmAppointment(int id);
        HttpResponse Create(AppointmentScheduleRequest request);
        HttpResponse Update(AppointmentScheduleRequest request);
        HttpResponse Delete(int id);
    }
}
