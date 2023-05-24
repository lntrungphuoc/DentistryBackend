using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookDetailService;
using AppDentistry.Models.Response.EHealthBookDetailService;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class EHealthBookDetailServiceServices : IEHealthBookDetailServiceServices
    {
        private readonly AppDbContext _dbContext;

        public EHealthBookDetailServiceServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse Create(EHealthBookDetailServiceRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var eHealthBookDetailService = new EHealthBookDetailService
            {
                IdEHealthBookDetail = request.IdEHealthBookDetail,
                IdService = request.IdService,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy
            };

            _dbContext.EHealthBookDetailServices.Add(eHealthBookDetailService);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Thêm dịch vụ thành công");
        }

        public HttpResponse Delete(int id)
        {
            var eHealthBookDetailService = _dbContext.EHealthBookDetailServices.FirstOrDefault(x => x.Id == id);

            if (eHealthBookDetailService == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.EHealthBookDetailServices.Remove(eHealthBookDetailService);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa dịch vụ thành công");
        }

        public HttpResponse DeleteRange(int idEHealthBookDetail)
        {
            var list = _dbContext.EHealthBookDetailServices.Where(x => x.IdEHealthBookDetail == idEHealthBookDetail).ToList();

            foreach (var service in list)
            {
                _dbContext.EHealthBookDetailServices.Remove(service);
                _dbContext.SaveChanges();
            }

            return HttpResponse.OK(message: "Xóa dịch vụ trong chi tiết sổ sức khỏe thành công");
        }

        public List<EHealthBookDetailServiceResponse> GetByEHealthBookDetailId(int id)
        {
            return _dbContext.EHealthBookDetailServices.Where(x => x.IdEHealthBookDetail == id)
                    .Select(x => new EHealthBookDetailServiceResponse
                    {
                        Id = x.Id,
                        IdEHealthBookDetail = x.IdEHealthBookDetail,
                        IdService = x.IdService,
                        CreatedDate = x.CreatedDate,
                        CreatedBy = x.CreatedBy,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedBy = x.ModifiedBy,
                        EHealthBookDetail = new Models.Response.EHealthBookDetail.EHealthBookDetailResponse
                        {
                            Id = x.IdEHealthBookDetail,
                            Diagnose = x.EHealthBookDetail.Diagnose,
                            Medicine = x.EHealthBookDetail.Medicine,
                            IdEHealthBook = x.EHealthBookDetail.IdEHealthBook,
                            CreatedDate = x.EHealthBookDetail.CreatedDate,
                            CreatedBy = x.EHealthBookDetail.CreatedBy,
                            ModifiedDate = x.EHealthBookDetail.ModifiedDate,
                            ModifiedBy = x.EHealthBookDetail.ModifiedBy
                        },
                        Service = new Models.Response.Service.ServiceResponse
                        {
                            Id = x.IdService,
                            Name = x.Service.Name,
                            Information = x.Service.Information,
                            Fee = x.Service.Fee,
                            URL = x.Service.URL,
                            CreatedDate = x.Service.CreatedDate,
                            CreatedBy = x.Service.CreatedBy,
                            ModifiedDate = x.Service.ModifiedDate,
                            ModifiedBy = x.Service.ModifiedBy

                        }
                    }).ToList();
        }

        public HttpResponse Update(EHealthBookDetailServiceRequest request)
        {
            var eHealthBookDetailService = _dbContext.EHealthBookDetailServices.FirstOrDefault(x => x.Id == request.Id);

            if (eHealthBookDetailService == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            eHealthBookDetailService.IdEHealthBookDetail = request.IdEHealthBookDetail;
            eHealthBookDetailService.IdService = request.IdService;
            eHealthBookDetailService.ModifiedDate = DateTime.Now;
            eHealthBookDetailService.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Cập nhật thành công");
        }
    }
}
