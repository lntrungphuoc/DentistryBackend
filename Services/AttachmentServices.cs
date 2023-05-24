using AppDentistry.Common.Providers;
using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Attachment;
using AppDentistry.Models.Response.Attachment;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public AttachmentServices(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }

        public HttpResponse Create(ListAttachmentRequest listRequest)
        {
            if (listRequest == null || listRequest.ListURL == null)
                return HttpResponse.Error("Có lỗi xảy ra, vui lòng thử lại sau.");

            //var ls = new List<Attachment>();
            //foreach (var item in listRequest.ListURL)
            //{
            //    var attachment = new Attachment
            //    {
            //        URL = FileProviders.UploadFile(item, _environment, "attachment"),
            //        IdEHealthBookDetail = listRequest.IdEHealthBookDetail,
            //        CreatedDate = DateTime.Now,
            //        CreatedBy = 1
            //    };
            //    ls.Add(attachment);
            //}

            var attachment = new Attachment
            {
                URL = FileProviders.UploadFile(listRequest.ListURL, _environment, "attachment"),
                IdEHealthBookDetail = listRequest.IdEHealthBookDetail,
                FileName = listRequest.ListURL.FileName,
                CreatedDate = DateTime.Now,
                CreatedBy = 1
            };

            //_dbContext.Attachments.AddRange(ls);
            _dbContext.Attachments.Add(attachment);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Thêm mới đính kèm thành công");
        }

        public List<AttachmentResponse> GetByEHealthBookDetail(int id)
        {
            return _dbContext.Attachments.Where(x => x.IdEHealthBookDetail == id)
                    .Select(x => new AttachmentResponse
                    {
                        Id = x.Id,
                        URL = x.URL,
                        IdEHealthBookDetail = x.IdEHealthBookDetail,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedBy = x.ModifiedBy
                    }).ToList();
        }

        public HttpResponse UpdateByEHealthBookDetail(List<AttachmentRequest> listAttachment, int idUser, int idEHealthBookDetail)
        {
            if (listAttachment.Count == 0)
            {
                var existAttachments = _dbContext.Attachments.Where(x => x.IdEHealthBookDetail == idEHealthBookDetail).ToList();
                _dbContext.Attachments.RemoveRange(existAttachments);
            }
            else
            {
                var allAttachments = _dbContext.Attachments.ToList();
                foreach (var attachment in listAttachment)
                {
                    var existAttachment = allAttachments.Where(x => x.Id == attachment.Id).FirstOrDefault();
                    allAttachments.Remove(existAttachment);
                }
                _dbContext.RemoveRange(allAttachments);
            }
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Cập nhật đính kèm thành công");
        }
    }
}
