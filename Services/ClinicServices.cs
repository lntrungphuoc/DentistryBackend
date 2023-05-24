using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Clinic;
using AppDentistry.Models.Response.Clinic;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class ClinicServices : IClinicServices
    {
        private readonly AppDbContext _dbContext;

        public ClinicServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse Create(ClinicRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var clinic = new Clinic
            {
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy
            };
            _dbContext.Clinics.Add(clinic);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Tạo mới phòng khám thành công");
        }

        public HttpResponse Delete(int id)
        {
            var clinic = _dbContext.Clinics.FirstOrDefault(x => x.Id == id);

            if (clinic == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.Clinics.Remove(clinic);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Xóa phòng khám thành công");
        }

        public List<ClinicResponse> GetAll()
        {
            return _dbContext.Clinics.Select(x => new ClinicResponse
            {
                Id = x.Id,
                Address = x.Address,
                PhoneNumber= x.PhoneNumber,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
        }

        public HttpResponse Update(ClinicRequest request)
        {
            var clinic = _dbContext.Clinics.FirstOrDefault(x => x.Id == request.Id);

            if (clinic == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            clinic.Address = request.Address;
            clinic.PhoneNumber = request.PhoneNumber;
            clinic.ModifiedDate = DateTime.Now;
            clinic.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Cập nhật thông tin phòng khám thành công");
        }
    }
}
