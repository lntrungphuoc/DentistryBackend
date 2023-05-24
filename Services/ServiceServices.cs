using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Service;
using AppDentistry.Models.Response.Service;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;
namespace AppDentistry.Services
{
    public class ServiceServices : IServiceServices
    {
        private readonly AppDbContext _dbContext;

        public ServiceServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public float CalculateFee(List<ServiceRequest> requests)
        {
            if (requests == null)
                return 0;
            else
            {
                float fee = 0;
                foreach (var service in requests)
                {
                    var s = _dbContext.Services.FirstOrDefault(x => x.Id == service.Id);
                    fee += s.Fee;
                }
                return fee;
            }
        }

        public HttpResponse Create(ServiceRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var service = new Service
            {
                Name = request.Name,
                Information = request.Information,
                Fee = request.Fee,
                URL = "",
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy,
            };

            _dbContext.Services.Add(service);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Tạo mới dịch vụ thành công");
        }

        public HttpResponse Delete(int id)
        {
            var service = _dbContext.Services.FirstOrDefault(s => s.Id == id);

            if (service == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.Services.Remove(service);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa dịch vụ thành công");
        }

        public List<ServiceResponse> GetAll()
        {
            return _dbContext.Services.OrderByDescending(x => x.CreatedDate).Select(x => new ServiceResponse
            {
                Id = x.Id,
                Name = x.Name,
                Information= x.Information,
                Fee = x.Fee,
                URL = x.URL,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
        }

        public ServiceResponse GetByURL(string url)
        {
            var service = _dbContext.Services.FirstOrDefault(x => x.URL == url);

            if (service == null)
                return null;
            else
                return new ServiceResponse
                {
                    Id = service.Id,
                    Name = service.Name,
                    Information = service.Information,
                    Fee = service.Fee,
                    URL = service.URL,
                    CreatedDate = service.CreatedDate,
                    CreatedBy = service.CreatedBy,
                    ModifiedDate = service.ModifiedDate,
                    ModifiedBy = service.ModifiedBy
                };
        }

        public HttpResponse Update(ServiceRequest request)
        {
            var service = _dbContext.Services.FirstOrDefault(s => s.Id == request.Id);

            if (service == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            service.Name = request.Name;
            service.Information = request.Information;
            service.Fee = request.Fee;
            service.ModifiedDate = DateTime.Now;
            service.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Cập nhật thông tin dịch vụ thành công");
        }
    }
}
