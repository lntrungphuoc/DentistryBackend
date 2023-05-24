using AppDentistry.Common.Providers;
using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Doctor;
using AppDentistry.Models.Response.Doctor;
using Domain;
using Domain.Entities;
using static AppDentistry.Common.AppConstants;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        private static IHttpContextAccessor _accessor;

        public DoctorServices(AppDbContext dbContext, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _environment = environment;
            _accessor = httpContextAccessor;
        }

        public static HttpContext HttpContext => _accessor.HttpContext;

        public HttpResponse Create(DoctorRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var doctor = new Doctor
            {
                Name = request.Name,
                Information = request.Information,
                AvatarUrl = FileProviders.UploadFile(request.AvatarUrl, _environment, DefaultAttribute.CONTENT_IMAGE_PATH),
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy,
            };

            _dbContext.Doctors.Add(doctor);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Tạo mới bác sĩ thành công");
        }

        public HttpResponse Delete(int id)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(x => x.Id == id);

            if (doctor == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.Doctors.Remove(doctor);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Xóa bác sĩ thành công");
        }

        public List<DoctorResponse> GetAll()
        {
            return _dbContext.Doctors.Select(x => new DoctorResponse
            {
                Id = x.Id,
                Name = x.Name,
                Information = x.Information,
                IsDoctor = x.IsDoctor,
                AvatarUrl= "https://" + HttpContext.Request.Host.Value + x.AvatarUrl,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy,
            }).ToList();
        }

        public DoctorResponse GetById(int id)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(x => x.Id == id);

            if (doctor == null)
                return null;
            else
                return new DoctorResponse
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Information = doctor.Information,
                    IsDoctor = doctor.IsDoctor,
                    AvatarUrl = "https://" + HttpContext.Request.Host.Value + doctor.AvatarUrl,
                    CreatedDate = doctor.CreatedDate,
                    CreatedBy = doctor.CreatedBy,
                    ModifiedDate = doctor.ModifiedDate,
                    ModifiedBy = doctor.ModifiedBy
                };
        }

        public HttpResponse Update(DoctorRequest request)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(x => x.Id == request.Id);

            if (doctor == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            doctor.Name = request.Name;
            doctor.Information = request.Information;
            doctor.IsDoctor = request.IsDoctor;
            if (request.AvatarUrl != null)
                doctor.AvatarUrl = FileProviders.UploadFile(request.AvatarUrl, _environment, DefaultAttribute.CONTENT_IMAGE_PATH);
            doctor.ModifiedDate = DateTime.Now;
            doctor.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Cập nhật thông tin bác sĩ thành công");
        }
    }
}
