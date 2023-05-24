using AppDentistry.Common.Providers;
using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.News;
using AppDentistry.Models.Response.News;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Services
{
    public class NewsServices : INewsServices
    {
        private readonly AppDbContext _dbContext;
        private static IHttpContextAccessor _accessor;

        public NewsServices(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _accessor = httpContextAccessor;
        }

        public static HttpContext HttpContext => _accessor.HttpContext;

        public HttpResponse Create(NewsRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var news = new News
            {
                Title = request.Title,
                Content = request.Content,
                URL = StringUtils.GenerateUrlTitle(request.Title),
                Thumbnail = request.Thumbnail,
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy,
            };

            _dbContext.News.Add(news);
            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Tạo bài đăng thành công");
        }

        public HttpResponse Delete(int id)
        {
            var news = _dbContext.News.FirstOrDefault(x => x.Id == id);

            if (news == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.News.Remove(news);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa bài viết thành công");
        }

        public List<NewsResponse> GetAll()
        {
            return _dbContext.News.OrderByDescending(x => x.CreatedDate).Select(x => new NewsResponse
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                URL = x.URL,
                Thumbnail = "https://" + HttpContext.Request.Host.Value + x.Thumbnail,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
        }

        public NewsResponse GetByURL(string url)
        {
            var news = _dbContext.News.FirstOrDefault(x => x.URL == url);

            if (news == null)
                return null;
            else
                return new NewsResponse
                {
                    Id = news.Id,
                    Title = news.Title,
                    Content = news.Content,
                    URL = news.URL,
                    Thumbnail = "https://" + HttpContext.Request.Host.Value + news.Thumbnail,
                    CreatedDate = news.CreatedDate,
                    CreatedBy = news.CreatedBy,
                    ModifiedDate = news.ModifiedDate,
                    ModifiedBy = news.ModifiedBy
                };
        }

        public List<NewsResponse> GetTop3Newest()
        {
            return _dbContext.News.Where(x => x.ForMobile == false).OrderByDescending(x => x.CreatedDate).Take(3).Select(x => new NewsResponse
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                URL = x.URL,
                Thumbnail = "https://" + HttpContext.Request.Host.Value + x.Thumbnail,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
        }

        public HttpResponse Update(NewsRequest request)
        {
            var news = _dbContext.News.FirstOrDefault(x => x.Id == request.Id);

            if (news == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            news.Title = request.Title;
            news.Content = request.Content;
            news.Thumbnail = request.Thumbnail;
            news.ModifiedDate = DateTime.Now;
            news.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Cập nhật thông tin bài viết thành công");
        }

        public List<NewsResponse> GetForWeb()
        {
            return _dbContext.News.Where(x => x.ForMobile == false).OrderByDescending(x => x.CreatedDate)
                .Select(x => new NewsResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    URL = x.URL,
                    Thumbnail = "https://" + HttpContext.Request.Host.Value + x.Thumbnail,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy
                }).ToList();
        }
    }
}
