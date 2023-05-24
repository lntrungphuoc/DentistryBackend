using AppDentistry.Common.Providers;
using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.PriceList;
using AppDentistry.Models.Response.PriceList;
using Domain;
using Domain.Entities;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;
namespace AppDentistry.Services
{
    public class PriceListServices : IPriceListServices
    {
        private readonly AppDbContext _dbContext;

        public PriceListServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponse Create(PriceListRequest request)
        {
            if (request == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            var priceList = new IPriceList
            {
                Title = request.Title,
                Content = request.Content,
                URL = StringUtils.GenerateUrlTitle(request.Title),
                CreatedDate = DateTime.Now,
                CreatedBy = request.CreatedBy,
            };

            _dbContext.PriceLists.Add(priceList);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Tạo mới bảng giá thành công");
        }

        public HttpResponse Delete(int id)
        {
            var priceList = _dbContext.PriceLists.FirstOrDefault(x => x.Id == id);

            if (priceList == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            _dbContext.PriceLists.Remove(priceList);
            _dbContext.SaveChanges();

            return HttpResponse.OK(message: "Xóa bảng giá thành công");
        }

        public List<PriceListResponse> GetAll()
        {
            return _dbContext.PriceLists.Select(x => new PriceListResponse
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                URL = x.URL,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
        }

        public PriceListResponse GetByURL(string url)
        {
            var priceList = _dbContext.PriceLists.FirstOrDefault(x => x.URL == url);

            if (priceList == null)
                return null;
            else
                return new PriceListResponse
                {
                    Id = priceList.Id,
                    Title = priceList.Title,
                    Content = priceList.Content,
                    URL = priceList.URL,
                    CreatedDate = priceList.CreatedDate,
                    CreatedBy = priceList.CreatedBy,
                    ModifiedDate = priceList.ModifiedDate,
                    ModifiedBy = priceList.ModifiedBy
                };
        }

        public HttpResponse Update(PriceListRequest request)
        {
            var priceList = _dbContext.PriceLists.FirstOrDefault(x => x.Id == request.Id);

            if (priceList == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại!");

            priceList.Title = request.Title;
            priceList.Content = request.Content;
            priceList.ModifiedDate = DateTime.Now;
            priceList.ModifiedBy = request.ModifiedBy;

            _dbContext.SaveChanges();
            return HttpResponse.OK(message: "Cập nhật thông tin bảng giá thành công");
        }
    }
}
