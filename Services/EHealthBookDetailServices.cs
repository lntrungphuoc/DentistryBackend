using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookDetail;
using AppDentistry.Models.Response.EHealthBookDetail;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class EHealthBookDetailServices : IEHealthBookDetailServices
    {
        private readonly AppDbContext _dbContext;
        private static IHttpContextAccessor _accessor;

        public EHealthBookDetailServices(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _accessor = httpContextAccessor;    
        }

        public static HttpContext HttpContext => _accessor.HttpContext;
    

    public HttpResponse Create(EHealthBookDetailRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var eHealthBookDetail = new EHealthBookDetail
            {
                Diagnose = request.Diagnose,
                Medicine = request.Medicine,
                IdEHealthBook = request.IdEHealthBook,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy
            };

            _dbContext.EHealthBookDetails.Add(eHealthBookDetail);
            _dbContext.SaveChanges();

            var newEHealthBookDetail = _dbContext.EHealthBookDetails.OrderByDescending(x => x.CreatedDate).Select(x => new EHealthBookDetailResponse
            {
                Id = x.Id,
                Diagnose = x.Diagnose,
                Medicine = x.Medicine,
                IdEHealthBook = x.IdEHealthBook,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy,
                EHealthBookDetailDoctor = _dbContext.EHealthBookDetailDoctors.Where(d => d.IdEHealthBookDetail == x.Id)
                                                .OrderByDescending(x => x.CreatedDate)
                                                .Select(e => new Models.Response.EHealthBookDetailDoctor.EHealthBookDetailDoctorResponse
                                                {
                                                    Id = e.Id,
                                                    IdDoctor = e.IdDoctor,
                                                    IdEHealthBookDetail = e.IdEHealthBookDetail,
                                                    CreatedDate = e.CreatedDate,
                                                    CreatedBy = e.CreatedBy,
                                                    ModifiedDate = e.ModifiedDate,
                                                    ModifiedBy = e.ModifiedBy
                                                }).ToList(),
                Attachments = _dbContext.Attachments.Where(a => a.IdEHealthBookDetail == x.Id)
                                                .OrderByDescending(a => a.CreatedDate)
                                                .Select(a => new Models.Response.Attachment.AttachmentResponse
                                                {
                                                    Id = a.Id,
                                                    IdEHealthBookDetail = a.IdEHealthBookDetail,
                                                    URL = a.URL,
                                                    FileName = a.FileName,
                                                    CreatedDate = a.CreatedDate,
                                                    CreatedBy = a.CreatedBy,
                                                    ModifiedDate = a.ModifiedDate,
                                                    ModifiedBy = a.ModifiedBy
                                                }).ToList(),
                EHealthBookDetailServices = _dbContext.EHealthBookDetailServices.Where(e => e.IdEHealthBookDetail == x.Id)
                                                .OrderByDescending(e => e.CreatedDate)
                                                .Select(e => new Models.Response.EHealthBookDetailService.EHealthBookDetailServiceResponse
                                                {
                                                    Id = e.Id,
                                                    IdEHealthBookDetail = e.IdEHealthBookDetail,
                                                    IdService = e.IdService,
                                                    CreatedDate = e.CreatedDate,
                                                    CreatedBy = e.CreatedBy,
                                                    ModifiedDate = e.ModifiedDate,
                                                    ModifiedBy = e.ModifiedBy,
                                                    Service = new Models.Response.Service.ServiceResponse
                                                    {
                                                        Id = e.IdService,
                                                        Name = e.Service.Name,
                                                        Information = e.Service.Information,
                                                        Fee = e.Service.Fee,
                                                        URL = e.Service.URL,
                                                        CreatedDate = e.Service.CreatedDate,
                                                        CreatedBy = e.Service.CreatedBy,
                                                        ModifiedDate = e.Service.ModifiedDate,
                                                        ModifiedBy = e.Service.ModifiedBy
                                                    }
                                                }).ToList()
            }).FirstOrDefault();

            return HttpResponse.OK(message: "Thêm mới thông tin khám vào sổ sức khỏe thành công", data: newEHealthBookDetail);
        }

        public HttpResponse Delete(int id)
        {
            var eHealthBookDetail = _dbContext.EHealthBookDetails.FirstOrDefault(x => x.Id == id);

            if (eHealthBookDetail == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.EHealthBookDetails.Remove(eHealthBookDetail);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa thông tin khám từ sổ sức khỏe thành công");
        }

        public List<EHealthBookDetailResponse> GetByIdHealthBook(int id)
        {
            var doctors = _dbContext.EHealthBookDetailDoctors.OrderByDescending(x => x.CreatedDate)
                                                .Select(e => new Models.Response.EHealthBookDetailDoctor.EHealthBookDetailDoctorResponse
                                                {
                                                    Id = e.Id,
                                                    IdDoctor = e.IdDoctor,
                                                    IdEHealthBookDetail = e.IdEHealthBookDetail,
                                                    CreatedDate = e.CreatedDate,
                                                    CreatedBy = e.CreatedBy,
                                                    ModifiedDate = e.ModifiedDate,
                                                    ModifiedBy = e.ModifiedBy,
                                                    Doctor = new Models.Response.Doctor.DoctorResponse
                                                    {
                                                        Id = e.IdDoctor,
                                                        Name = e.Doctor.Name,
                                                        Information = e.Doctor.Information,
                                                        AvatarUrl = e.Doctor.AvatarUrl,
                                                        IsDoctor = e.Doctor.IsDoctor
                                                    }
                                                });

            var attachments = _dbContext.Attachments.OrderByDescending(x => x.CreatedDate)
                                                .Select(a => new Models.Response.Attachment.AttachmentResponse
                                                {
                                                    Id = a.Id,
                                                    IdEHealthBookDetail = a.IdEHealthBookDetail,
                                                    URL = "https://" + HttpContext.Request.Host.Value + a.URL,
                                                    FileName = a.FileName,
                                                    CreatedDate = a.CreatedDate,
                                                    CreatedBy = a.CreatedBy,
                                                    ModifiedDate = a.ModifiedDate,
                                                    ModifiedBy = a.ModifiedBy
                                                });

            var services = _dbContext.EHealthBookDetailServices.OrderByDescending(x => x.CreatedDate)
                                                .Select(e => new Models.Response.EHealthBookDetailService.EHealthBookDetailServiceResponse
                                                {
                                                    Id = e.Id,
                                                    IdEHealthBookDetail = e.IdEHealthBookDetail,
                                                    IdService = e.IdService,
                                                    CreatedDate = e.CreatedDate,
                                                    CreatedBy = e.CreatedBy,
                                                    ModifiedDate = e.ModifiedDate,
                                                    ModifiedBy = e.ModifiedBy,
                                                    Service = new Models.Response.Service.ServiceResponse
                                                    {
                                                        Id = e.IdService,
                                                        Name = e.Service.Name,
                                                        Information = e.Service.Information,
                                                        Fee = e.Service.Fee,
                                                        URL = e.Service.URL,
                                                        CreatedDate = e.Service.CreatedDate,
                                                        CreatedBy = e.Service.CreatedBy,
                                                        ModifiedDate = e.Service.ModifiedDate,
                                                        ModifiedBy = e.Service.ModifiedBy
                                                    }
                                                });

            return _dbContext.EHealthBookDetails.Where(x => x.IdEHealthBook == id).OrderByDescending(x => x.CreatedDate)
                .Select(x => new EHealthBookDetailResponse
            {
                Id = x.Id,
                Diagnose = x.Diagnose,
                Medicine = x.Medicine,
                IdEHealthBook = x.IdEHealthBook,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy,
                EHealthBookDetailDoctor = doctors.Where(d => d.IdEHealthBookDetail == x.Id).ToList(),
                Attachments = attachments.Where(a => a.IdEHealthBookDetail == x.Id).ToList(),
                EHealthBookDetailServices = services.Where(s => s.IdEHealthBookDetail == x.Id).ToList()
            }).ToList();
        }

        public HttpResponse Update(EHealthBookDetailRequest request)
        {
            var eHealthBookDetail = _dbContext.EHealthBookDetails.FirstOrDefault(x => x.Id == request.Id);

            if (eHealthBookDetail == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            eHealthBookDetail.Diagnose = request.Diagnose;
            eHealthBookDetail.Medicine = request.Medicine;
            eHealthBookDetail.IdEHealthBook = request.IdEHealthBook;
            eHealthBookDetail.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Thay đổi thông tin khám từ sổ sức khỏe thành công");
        }
    }
}
