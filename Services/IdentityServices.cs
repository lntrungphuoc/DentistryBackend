using AppDentistry.Common.Http;
using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.User;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AppDentistry.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly UserManager<User> _userManager;

        public IdentityServices(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //public async Task<HttpResponse<User>> LoginAsync(LoginDTO loginInfo)
        //{
        //    var user = await _userManager
        //        .Users
        //        .FirstOrDefaultAsync(u => u.UserName == loginInfo.Username && u.EmailConfirmed == true);
        //    string message = string.Empty;

        //    if (user != null)
        //    {
        //        bool passwordIsValid = await _userManager.CheckPasswordAsync(user, loginInfo.Password);
        //        if (passwordIsValid)
        //        {

        //            return HttpResponse<User>.OK(user);
        //        }
        //        else
        //        {
        //            message = "Mật khẩu không chính xác";
        //        }
        //    }
        //    else
        //    {
        //        // User invalid
        //        message = "Tài khoản không tồn tại";
        //    }

        //    return HttpResponse<User>.Error(message, HttpStatusCode.BadRequest);
        //}

        public async Task<HttpResponse<User>> LoginAsync(LoginDto loginInfo)
        {
            var user = await _userManager
                .Users
                .FirstOrDefaultAsync(u => u.UserName == loginInfo.Username && u.EmailConfirmed == true);
            string message = string.Empty;

            if (user != null)
            {
                bool passwordIsValid = await _userManager.CheckPasswordAsync(user, loginInfo.Password);
                if (passwordIsValid)
                {

                    return HttpResponse<User>.OK(user);
                }
                else
                {
                    message = "Mật khẩu không chính xác";
                }
            }
            else
            {
                // User invalid
                message = "Tài khoản không tồn tại";
            }

            return HttpResponse<User>.Error(message, HttpStatusCode.BadRequest);
        }
    }
}
