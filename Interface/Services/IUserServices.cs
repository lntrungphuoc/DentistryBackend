using AppDentistry.Models.Request.User;
using AppDentistry.Models.Response.User;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Interface.Services
{
    public interface IUserServices
    {
        Task<HttpResponse> LoginAsync(LoginRequest loginInfo);
        UserResponse GetLoggedInfo(string userName);
    }
}
