using AppDentistry.Models.Request.News;
using AppDentistry.Models.Response.News;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface INewsServices
    {
        List<NewsResponse> GetAll();
        List<NewsResponse> GetTop3Newest();
        List<NewsResponse> GetForWeb();
        NewsResponse GetByURL(string url);
        HttpResponse Create(NewsRequest request);
        HttpResponse Update(NewsRequest request);
        HttpResponse Delete(int id);
    }
}
