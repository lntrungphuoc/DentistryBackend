using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookService;
using AppDentistry.Models.Response.EHealthBookService;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class EHealthBookServiceServices : IEHealthBookServiceServices
    {
        private readonly AppDbContext _dbContext;

        public EHealthBookServiceServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse Create(EHealthBookServiceRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var eHealthBookService = new EHealthBookService
            {
                IdEHealthBook = request.IdEHealthBook,
                IdService = request.IdService,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy
            };

            _dbContext.EHealthBookServices.Add(eHealthBookService);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Thêm dịch vụ mới vào sổ sức khỏe thành công");
        }

        public HttpResponse Delete(int id)
        {
            var eHealthBookService = _dbContext.EHealthBookServices.FirstOrDefault(x => x.Id == id);

            if (eHealthBookService == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.EHealthBookServices.Remove(eHealthBookService);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa dịch vụ từ sổ sức khỏe thành công");
        }

        public HttpResponse DeleteRange(int idEHealthBook)
        {
            var eHealthBookServices = _dbContext.EHealthBookServices.Where(x => x.IdEHealthBook == idEHealthBook).ToList();

            _dbContext.EHealthBookServices.RemoveRange(eHealthBookServices);
            _dbContext.SaveChanges();

            return HttpResponse.OK();
        }

        public List<EHealthBookServiceResponse> GetByEHealthBookId(int id)
        {
            return _dbContext.EHealthBookServices.Where(x => x.IdEHealthBook == id)
                    .Select(x => new EHealthBookServiceResponse
                    {
                        Id = x.Id,
                        IdEHealthBook = x.IdEHealthBook,
                        IdService = x.IdService,
                        CreatedDate = x.CreatedDate,
                        CreatedBy = x.CreatedBy,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedBy = x.ModifiedBy,
                        EHealthBook = new Models.Response.EHealthBook.EHealthBookResponse()
                        {
                            Id = x.IdEHealthBook,
                            TotalFee = x.EHealthBook.TotalFee,
                            IdClinic = x.EHealthBook.IdClinic,
                            IdCustomer = x.EHealthBook.IdCustomer,
                            CheckUpDate = x.EHealthBook.CheckUpDate,
                            ReExaminationDate = x.EHealthBook.ReExaminationDate,
                            Note = x.EHealthBook.Note,
                            CreatedDate = x.EHealthBook.CreatedDate,
                            CreatedBy = x.EHealthBook.CreatedBy,
                            ModifiedDate = x.EHealthBook.ModifiedDate,
                            ModifiedBy = x.EHealthBook.ModifiedBy
                        },
                        Service = new Models.Response.Service.ServiceResponse()
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

        public HttpResponse Update(EHealthBookServiceRequest request)
        {
            var eHealthBookService = _dbContext.EHealthBookServices.FirstOrDefault(x => x.Id == request.Id);

            if (eHealthBookService == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            eHealthBookService.IdService = request.IdService;
            eHealthBookService.IdEHealthBook = request.IdEHealthBook;
            eHealthBookService.ModifiedDate = DateTime.Now;
            eHealthBookService.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Cập nhật thông tin thành công");
        }
    }
}
