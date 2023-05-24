using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.AppointmentSchedule;
using AppDentistry.Models.Response.AppointmentSchedule;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class AppointmentScheduleServices : IAppointmentScheduleServices
    {
        private readonly AppDbContext _dbContext;

        public AppointmentScheduleServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse ConfirmAppointment(int id)
        {
            if (id == 0)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var appointmentSchedule = _dbContext.AppointmentSchedules.FirstOrDefault(x => x.Id == id);

            if (appointmentSchedule == null)
            {
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");
            }
            else
            {
                appointmentSchedule.IsConfirm = true;
                _dbContext.SaveChanges();
            }
            return HttpResponse.OK(message: "Xác nhận lịch hẹn thành công");
        }

        public HttpResponse Create(AppointmentScheduleRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var appointmentSchedule = new AppointmentSchedule
            {
                IdClinic = request.IdClinic,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Date = request.Date,
                Time = request.Time,
                Content = request.Content,
                IsConfirm = false,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy
            };

            _dbContext.AppointmentSchedules.Add(appointmentSchedule);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Đặt lịch hẹn thành công", data: GetAll());
        }

        public HttpResponse Delete(int id)
        {
            var appointmentSchedule = _dbContext.AppointmentSchedules.FirstOrDefault(x => x.Id == id);

            if (appointmentSchedule == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.AppointmentSchedules.Remove(appointmentSchedule);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Xóa lịch hẹn thành công");
        }

        public List<AppointmentScheduleResponse> GetAll()
        {
            return _dbContext.AppointmentSchedules.OrderByDescending(x => x.CreatedDate).Select(x => new AppointmentScheduleResponse
            {
                Id = x.Id,
                IdClinic = x.IdClinic,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Date = x.Date,
                Time = x.Time,
                Content = x.Content,
                IsConfirm = x.IsConfirm,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                Clinic = new Models.Response.Clinic.ClinicResponse()
                {
                    Id = x.IdClinic,
                    Address = x.Clinic.Address,
                    PhoneNumber = x.Clinic.PhoneNumber
                }
            }).ToList();
        }

        public AppointmentScheduleResponse GetById(int id)
        {
            var appointmentSchedule = _dbContext.AppointmentSchedules.FirstOrDefault(x => x.Id == id);

            if (appointmentSchedule == null)
                return null;
            else
                return new AppointmentScheduleResponse
                {
                    Id = appointmentSchedule.Id,
                    IdClinic = appointmentSchedule.IdClinic,
                    Name = appointmentSchedule.Name,
                    PhoneNumber = appointmentSchedule.PhoneNumber,
                    Date = appointmentSchedule.Date,
                    Time = appointmentSchedule.Time,
                    Content = appointmentSchedule.Content,
                    IsConfirm = appointmentSchedule.IsConfirm,
                    CreatedDate = appointmentSchedule.CreatedDate,
                    CreatedBy = appointmentSchedule.CreatedBy,
                    ModifiedBy = appointmentSchedule.ModifiedBy,
                    ModifiedDate = appointmentSchedule.ModifiedDate,
                    Clinic = new Models.Response.Clinic.ClinicResponse()
                    {
                        Id = appointmentSchedule.IdClinic,
                        Address = appointmentSchedule.Clinic.Address,
                        PhoneNumber = appointmentSchedule.Clinic.PhoneNumber
                    }
                };
        }

        public List<AppointmentScheduleResponse> GetConfirmedAppointment()
        {
            return _dbContext.AppointmentSchedules.OrderByDescending(x => x.CreatedDate).Where(x => x.IsConfirm == true)
                .Select(x => new AppointmentScheduleResponse
                {
                    Id = x.Id,
                    IdClinic = x.IdClinic,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Date = x.Date,
                    Time = x.Time,
                    Content = x.Content,
                    IsConfirm = x.IsConfirm,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate,
                    Clinic = new Models.Response.Clinic.ClinicResponse()
                    {
                        Id = x.IdClinic,
                        Address = x.Clinic.Address,
                        PhoneNumber = x.Clinic.PhoneNumber
                    }
                }).ToList();
        }

        public List<AppointmentScheduleResponse> GetHaveNotConfirmedAppointment()
        {
            return _dbContext.AppointmentSchedules.OrderByDescending(x => x.CreatedDate).Where(x => x.IsConfirm == false)
                .Select(x => new AppointmentScheduleResponse
                {
                    Id = x.Id,
                    IdClinic = x.IdClinic,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Date = x.Date,
                    Time = x.Time,
                    Content = x.Content,
                    IsConfirm = x.IsConfirm,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate,
                    Clinic = new Models.Response.Clinic.ClinicResponse()
                    {
                        Id = x.IdClinic,
                        Address = x.Clinic.Address,
                        PhoneNumber = x.Clinic.PhoneNumber
                    }
                }).ToList();
        }

        public HttpResponse Update(AppointmentScheduleRequest request)
        {
            var appointmentSchedule = _dbContext.AppointmentSchedules.FirstOrDefault(x => x.Id == request.Id);

            if (appointmentSchedule == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            appointmentSchedule.IdClinic = request.IdClinic;
            appointmentSchedule.Name = request.Name;
            appointmentSchedule.PhoneNumber = request.PhoneNumber;
            appointmentSchedule.Date = request.Date;
            appointmentSchedule.Time = request.Time;
            appointmentSchedule.Content = request.Content;
            appointmentSchedule.ModifiedDate = DateTime.Now;
            appointmentSchedule.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Chỉnh sửa lịch hẹn thành công");
        }
    }
}
