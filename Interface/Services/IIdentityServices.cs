using AppDentistry.Common.Http;
using AppDentistry.Models.Request.User;
using Domain.Entities;

namespace AppDentistry.Interface.Services
{
    public interface IIdentityServices
    {
        Task<HttpResponse<User>> LoginAsync(LoginDto loginInfo);
    }
}
