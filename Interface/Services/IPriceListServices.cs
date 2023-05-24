using AppDentistry.Models.Request.PriceList;
using AppDentistry.Models.Response.PriceList;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;
namespace AppDentistry.Interface.Services
{
    public interface IPriceListServices
    {
        List<PriceListResponse> GetAll();
        PriceListResponse GetByURL(string url);
        HttpResponse Create(PriceListRequest request);
        HttpResponse Update(PriceListRequest request);
        HttpResponse Delete(int id);
    }
}
