using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookDetailDoctor;
using AppDentistry.Models.Response.EHealthBookDetailDoctor;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class EHealthBookDetailDoctorServices : IEHealthBookDetailDoctorServices
    {
        private readonly AppDbContext _dbContext;

        public EHealthBookDetailDoctorServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse Create(EHealthBookDetailDoctorRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var eHealthBookDetailDoctor = new EHealthBookDetailDoctor
            {
                IdDoctor = request.IdDoctor,
                IdEHealthBookDetail = request.IdEHealthBookDetail,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy
            };

            _dbContext.EHealthBookDetailDoctors.Add(eHealthBookDetailDoctor);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Thêm bác sĩ vào sổ sức khỏe thành công");
        }

        public HttpResponse Delete(int id)
        {
            var eHealthBookDetailDoctor = _dbContext.EHealthBookDetailDoctors.FirstOrDefault(x => x.Id == id);

            if (eHealthBookDetailDoctor == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.EHealthBookDetailDoctors.Remove(eHealthBookDetailDoctor);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa bác sĩ từ sổ sức khỏe thành công");
        }

        public HttpResponse DeleteRange(int idEHealthBookDetail)
        {
            var list = _dbContext.EHealthBookDetailDoctors.Where(x => x.IdEHealthBookDetail == idEHealthBookDetail).ToList();

            foreach (var dr in list)
            {
                _dbContext.EHealthBookDetailDoctors.Remove(dr);
                _dbContext.SaveChanges();
            }

            return HttpResponse.OK(message: "Xóa bác sĩ từ chi tiết sổ sức khỏe thành công");
        }

        public List<EHealthBookDetailDoctorResponse> GetByEHealthBookDetailId(int id)
        {
            return _dbContext.EHealthBookDetailDoctors.Where(x => x.IdEHealthBookDetail == id)
                    .OrderByDescending(x => x.CreatedDate)
                    .Select(x => new EHealthBookDetailDoctorResponse
                    {
                        Id = x.Id,
                        IdDoctor = x.IdDoctor,
                        IdEHealthBookDetail = x.IdEHealthBookDetail,
                        CreatedDate = x.CreatedDate,
                        CreatedBy = x.CreatedBy,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedBy = x.ModifiedBy,
                        Doctor = new Models.Response.Doctor.DoctorResponse
                        {
                            Id = x.IdDoctor,
                            Name = x.Doctor.Name,
                            Information = x.Doctor.Information,
                            IsDoctor = x.Doctor.IsDoctor,
                            AvatarUrl = x.Doctor.AvatarUrl,
                            CreatedDate = x.Doctor.CreatedDate,
                            CreatedBy = x.Doctor.CreatedBy,
                            ModifiedDate = x.Doctor.ModifiedDate,
                            ModifiedBy = x.Doctor.ModifiedBy
                        },
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
                        }
                    }).ToList();
        }

        public List<EHealthBookDetailDoctorResponse> GetEHealthBookDetailByDoctorId(int idUser)
        {
            throw new NotImplementedException();
        }

        //public List<EHealthBookDetailDoctorResponse> GetEHealthBookDetailByDoctorId(int idUser)
        //{
        //    var user = _dbContext.Users.FirstOrDefault(x => x.Id == idUser);
        //    return _dbContext.EHealthBookDetailDoctors.Where(x => x.IdDoctor == user.IdDoctor).Select(x => new EHealthBookDetailDoctorResponse
        //    {
        //        Id = x.Id,
        //        IdDoctor = x.IdDoctor,
        //        IdEHealthBookDetail = x.IdEHealthBookDetail,
        //        CreatedDate = x.CreatedDate,
        //        CreatedBy = x.CreatedBy,
        //        ModifiedDate = x.ModifiedDate,
        //        ModifiedBy = x.ModifiedBy,
        //        Doctor = new Models.Response.Doctor.DoctorResponse
        //        {
        //            Id = x.IdDoctor,
        //            Name = x.Doctor.Name,
        //            Information = x.Doctor.Information,
        //            IsDoctor = x.Doctor.IsDoctor,
        //            AvatarUrl = x.Doctor.AvatarUrl,
        //            CreatedDate = x.Doctor.CreatedDate,
        //            CreatedBy = x.Doctor.CreatedBy,
        //            ModifiedDate = x.Doctor.ModifiedDate,
        //            ModifiedBy = x.Doctor.ModifiedBy
        //        },
        //        EHealthBookDetail = new Models.Response.EHealthBookDetail.EHealthBookDetailResponse
        //        {
        //            Id = x.IdEHealthBookDetail,
        //            CheckUpDate = x.EHealthBookDetail.CheckUpDate,
        //            Diagnose = x.EHealthBookDetail.Diagnose,
        //            Medicine = x.EHealthBookDetail.Medicine,
        //            ReExaminationDate = x.EHealthBookDetail.ReExaminationDate,
        //            IdEHealthBook = x.EHealthBookDetail.IdEHealthBook,
        //            CreatedDate = x.EHealthBookDetail.CreatedDate,
        //            CreatedBy = x.EHealthBookDetail.CreatedBy,
        //            ModifiedDate = x.EHealthBookDetail.ModifiedDate,
        //            ModifiedBy = x.EHealthBookDetail.ModifiedBy
        //        }
        //    }).ToList();
        //}

        public HttpResponse Update(EHealthBookDetailDoctorRequest request)
        {
            var eHealthBookDetailDoctor = _dbContext.EHealthBookDetailDoctors.FirstOrDefault(x => x.Id == request.Id);

            if (eHealthBookDetailDoctor == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            eHealthBookDetailDoctor.IdDoctor = request.IdDoctor;
            eHealthBookDetailDoctor.IdEHealthBookDetail = request.IdEHealthBookDetail;
            eHealthBookDetailDoctor.ModifiedDate = DateTime.Now;
            eHealthBookDetailDoctor.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Cập nhật thông tin thành công");
        }
    }
}
