using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBook;
using AppDentistry.Models.Response.EHealthBook;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class EHealthBookServices : IEHealthBookServices
    {
        private readonly AppDbContext _dbContext;

        public EHealthBookServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse Create(EHealthBookRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var eHealthBook = new EHealthBook
            {
                TotalFee = request.TotalFee,
                CheckUpDate = request.CheckUpDate,
                IdClinic = request.IdClinic,
                IdCustomer = request.IdCustomer,
                ReExaminationDate = request.ReExaminationDate,
                Note = request.Note,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy
            };

            _dbContext.EHealthBooks.Add(eHealthBook);
            _dbContext.SaveChanges();

            var newEHealthBook = _dbContext.EHealthBooks.OrderByDescending(x => x.CreatedDate).Take(1);

            return HttpResponse.OK(message: "Tạo sổ điện tử thành công", data: newEHealthBook);
        }

        public HttpResponse Delete(int id)
        {
            var eHealthBook = _dbContext.EHealthBooks.FirstOrDefault(x => x.Id == id);

            if (eHealthBook == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.EHealthBooks.Remove(eHealthBook);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa sổ điện tử thành công");
        }

        public List<EHealthBookResponse> GetAll()
        {
            var eHealthBookServices = _dbContext.EHealthBookServices.OrderByDescending(e => e.CreatedDate)
                                             .Select(e => new Models.Response.EHealthBookService.EHealthBookServiceResponse
                                             {
                                                 Id = e.Id,
                                                 IdEHealthBook = e.IdEHealthBook,
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
                                                     URL = e.Service.URL
                                                 }
                                             });

            return _dbContext.EHealthBooks.OrderByDescending(x => x.CreatedDate)
                             .Select(x => new EHealthBookResponse
                             {
                                 Id = x.Id,
                                 TotalFee = x.TotalFee,
                                 IdCustomer = x.IdCustomer,
                                 IdClinic = x.IdClinic,
                                 CheckUpDate = x.CheckUpDate,
                                 ReExaminationDate = x.ReExaminationDate,
                                 Note = x.Note,
                                 CreatedDate = x.CreatedDate,
                                 CreatedBy = x.CreatedBy,
                                 ModifiedDate = x.ModifiedDate,
                                 ModifiedBy = x.ModifiedBy,
                                 Clinic = new Models.Response.Clinic.ClinicResponse
                                 {
                                     Id = x.IdClinic,
                                     Address = x.Clinic.Address,
                                     PhoneNumber = x.Clinic.PhoneNumber,
                                     CreatedDate = x.Clinic.CreatedDate,
                                     CreatedBy = x.Clinic.CreatedBy,
                                     ModifiedDate = x.Clinic.ModifiedDate,
                                     ModifiedBy = x.Clinic.ModifiedBy
                                 },
                                 Customer = new Models.Response.Customer.CustomerResponse
                                 {
                                     Id = x.IdCustomer,
                                     Address = x.Customer.Address,
                                     CustomerName = x.Customer.CustomerName,
                                     PhoneNumber = x.Customer.PhoneNumber,
                                     Password = x.Customer.Password,
                                     CCCD = x.Customer.CCCD,
                                     BHYT = x.Customer.BHYT,
                                     CreatedDate = x.Customer.CreatedDate,
                                     CreatedBy = x.Customer.CreatedBy,
                                     ModifiedDate = x.Customer.ModifiedDate,
                                     ModifiedBy = x.Customer.ModifiedBy
                                 },
                                 EHealthBookServices = eHealthBookServices.Where(e => e.IdEHealthBook == x.Id).ToList(),
                                 EHealthBookDetails = _dbContext.EHealthBookDetails.Where(x => x.IdEHealthBook == x.Id)
                                            .OrderByDescending(x => x.CreatedDate)
                                            .Select(x => new Models.Response.EHealthBookDetail.EHealthBookDetailResponse
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
                                                    ModifiedBy = e.ModifiedBy
                                                }).ToList()
                                            }).ToList()
                             }).ToList();
        }

        public List<EHealthBookResponse> GetById(int id)
        {
            return _dbContext.EHealthBooks.Where(x => x.Id == id).OrderByDescending(x => x.CreatedDate)
                .Select(x => new EHealthBookResponse
                {
                    Id = x.Id,
                    TotalFee = x.TotalFee,
                    IdCustomer = x.IdCustomer,
                    IdClinic = x.IdClinic,
                    CheckUpDate = x.CheckUpDate,
                    ReExaminationDate = x.ReExaminationDate,
                    Note = x.Note,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy,
                    Clinic = new Models.Response.Clinic.ClinicResponse
                    {
                        Id = x.IdClinic,
                        Address = x.Clinic.Address,
                        PhoneNumber = x.Clinic.PhoneNumber,
                        CreatedDate = x.Clinic.CreatedDate,
                        CreatedBy = x.Clinic.CreatedBy,
                        ModifiedDate = x.Clinic.ModifiedDate,
                        ModifiedBy = x.Clinic.ModifiedBy
                    },
                    Customer = new Models.Response.Customer.CustomerResponse
                    {
                        Id = x.IdCustomer,
                        Address = x.Customer.Address,
                        CustomerName = x.Customer.CustomerName,
                        PhoneNumber = x.Customer.PhoneNumber,
                        Password = x.Customer.Password,
                        CCCD = x.Customer.CCCD,
                        BHYT = x.Customer.BHYT,
                        CreatedDate = x.Customer.CreatedDate,
                        CreatedBy = x.Customer.CreatedBy,
                        ModifiedDate = x.Customer.ModifiedDate,
                        ModifiedBy = x.Customer.ModifiedBy
                    },
                    EHealthBookServices = _dbContext.EHealthBookServices.Where(e => e.IdEHealthBook == x.Id)
                                             .OrderByDescending(e => e.CreatedDate)
                                             .Select(e => new Models.Response.EHealthBookService.EHealthBookServiceResponse
                                             {
                                                 Id = e.Id,
                                                 IdEHealthBook = e.IdEHealthBook,
                                                 IdService = e.IdService,
                                                 CreatedDate = e.CreatedDate,
                                                 CreatedBy = e.CreatedBy,
                                                 ModifiedDate = e.ModifiedDate,
                                                 ModifiedBy = e.ModifiedBy
                                             }).ToList(),
                    EHealthBookDetails = _dbContext.EHealthBookDetails.Where(x => x.IdEHealthBook == x.Id)
                                            .OrderByDescending(x => x.CreatedDate)
                                            .Select(x => new Models.Response.EHealthBookDetail.EHealthBookDetailResponse
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
                                                    ModifiedBy = e.ModifiedBy
                                                }).ToList()
                                            }).ToList()
                }).ToList();
        }

        public List<EHealthBookResponse> GetByIdUser(int id)
        {
            return _dbContext.EHealthBooks.Where(x => x.IdCustomer == id).OrderByDescending(x => x.CreatedDate)
                .Select(x => new EHealthBookResponse
                {
                    Id = x.Id,
                    TotalFee = x.TotalFee,
                    IdCustomer = x.IdCustomer,
                    IdClinic = x.IdClinic,
                    CheckUpDate = x.CheckUpDate,
                    ReExaminationDate = x.ReExaminationDate,
                    Note = x.Note,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy,
                    Clinic = new Models.Response.Clinic.ClinicResponse
                    {
                        Id = x.IdClinic,
                        Address = x.Clinic.Address,
                        PhoneNumber = x.Clinic.PhoneNumber,
                        CreatedDate = x.Clinic.CreatedDate,
                        CreatedBy = x.Clinic.CreatedBy,
                        ModifiedDate = x.Clinic.ModifiedDate,
                        ModifiedBy = x.Clinic.ModifiedBy
                    },
                    Customer = new Models.Response.Customer.CustomerResponse
                    {
                        Id = x.IdCustomer,
                        Address = x.Customer.Address,
                        CustomerName = x.Customer.CustomerName,
                        PhoneNumber = x.Customer.PhoneNumber,
                        Password = x.Customer.Password,
                        CCCD = x.Customer.CCCD,
                        BHYT = x.Customer.BHYT,
                        CreatedDate = x.Customer.CreatedDate,
                        CreatedBy = x.Customer.CreatedBy,
                        ModifiedDate = x.Customer.ModifiedDate,
                        ModifiedBy = x.Customer.ModifiedBy
                    },
                    EHealthBookServices = _dbContext.EHealthBookServices.Where(e => e.IdEHealthBook == x.Id)
                                             .OrderByDescending(e => e.CreatedDate)
                                             .Select(e => new Models.Response.EHealthBookService.EHealthBookServiceResponse
                                             {
                                                 Id = e.Id,
                                                 IdEHealthBook = e.IdEHealthBook,
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
                                             }).ToList(),
                    EHealthBookDetails = _dbContext.EHealthBookDetails.Where(x => x.IdEHealthBook == x.Id)
                                            .OrderByDescending(x => x.CreatedDate)
                                            .Select(x => new Models.Response.EHealthBookDetail.EHealthBookDetailResponse
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
                                                    ModifiedBy = e.ModifiedBy
                                                }).ToList()
                                            }).ToList()
                }).ToList();
        }

        public EHealthBookResponse GetNewest()
        {
            return _dbContext.EHealthBooks.OrderByDescending(x => x.CreatedDate).Select(x => new EHealthBookResponse
            {
                Id = x.Id,
                TotalFee = x.TotalFee,
                IdCustomer = x.IdCustomer,
                IdClinic = x.IdClinic,
                CheckUpDate = x.CheckUpDate,
                ReExaminationDate = x.ReExaminationDate,
                Note = x.Note,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy,
                Clinic = new Models.Response.Clinic.ClinicResponse
                {
                    Id = x.IdClinic,
                    Address = x.Clinic.Address,
                    PhoneNumber = x.Clinic.PhoneNumber,
                    CreatedDate = x.Clinic.CreatedDate,
                    CreatedBy = x.Clinic.CreatedBy,
                    ModifiedDate = x.Clinic.ModifiedDate,
                    ModifiedBy = x.Clinic.ModifiedBy
                },
                Customer = new Models.Response.Customer.CustomerResponse
                {
                    Id = x.IdCustomer,
                    Address = x.Customer.Address,
                    CustomerName = x.Customer.CustomerName,
                    PhoneNumber = x.Customer.PhoneNumber,
                    Password = x.Customer.Password,
                    CCCD = x.Customer.CCCD,
                    BHYT = x.Customer.BHYT,
                    CreatedDate = x.Customer.CreatedDate,
                    CreatedBy = x.Customer.CreatedBy,
                    ModifiedDate = x.Customer.ModifiedDate,
                    ModifiedBy = x.Customer.ModifiedBy
                },
                EHealthBookServices = _dbContext.EHealthBookServices.Where(e => e.IdEHealthBook == x.Id)
                                             .OrderByDescending(e => e.CreatedDate)
                                             .Select(e => new Models.Response.EHealthBookService.EHealthBookServiceResponse
                                             {
                                                 Id = e.Id,
                                                 IdEHealthBook = e.IdEHealthBook,
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
                                             }).ToList(),
                EHealthBookDetails = _dbContext.EHealthBookDetails.Where(x => x.IdEHealthBook == x.Id)
                                            .OrderByDescending(x => x.CreatedDate)
                                            .Select(x => new Models.Response.EHealthBookDetail.EHealthBookDetailResponse
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
                                                    ModifiedBy = e.ModifiedBy
                                                }).ToList()
                                            }).ToList()
            }).First();
        }

        public HttpResponse Update(EHealthBookRequest request)
        {
            var eHealthBook = _dbContext.EHealthBooks.FirstOrDefault(x => x.Id == request.Id);

            if (eHealthBook == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            eHealthBook.Id = request.Id;
            eHealthBook.IdClinic = request.IdClinic;
            eHealthBook.IdCustomer = request.IdCustomer;
            eHealthBook.CheckUpDate = request.CheckUpDate;
            eHealthBook.TotalFee = request.TotalFee;
            eHealthBook.ReExaminationDate = request.ReExaminationDate;
            eHealthBook.Note = request.Note;
            eHealthBook.ModifiedDate = DateTime.Now;
            eHealthBook.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Đổi thông tin sổ điện tử thành công");
        }
    }
}
