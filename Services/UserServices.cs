using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.User;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using LoginDTO = AppDentistry.Models.Request.User.LoginDto;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;
using Domain;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using AppDentistry.Models.Response.User;
using System.Security.Principal;

namespace AppDentistry.Services
{
    public class UserServices : IUserServices
    {
        private readonly IIdentityServices _identityServices;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;

        public UserServices(IIdentityServices identityServices, UserManager<User> userManager, AppDbContext dbContext)
        {
            _identityServices = identityServices;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public UserResponse GetLoggedInfo(string userName)
        {
            var user = _dbContext.Users.Where(x => x.UserName == userName).Select(x => new UserResponse
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
            }).FirstOrDefault();

            return user;
        }

        public async Task<HttpResponse> LoginAsync(LoginRequest loginInfo)
        {
            if (loginInfo == null)
                return HttpResponse.Error(message: "Có lỗi xảy ra, vui lòng thử lại");

            var user = _dbContext.Users.FirstOrDefault(x => x.UserName == loginInfo.Username && x.PasswordHash == loginInfo.Password);
            
            if (user == null)
                return HttpResponse.Error(message: "Thông tin đăng nhập sai");
            else
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var lstRoles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Surname, user.FullName)
                };

                foreach (var item in lstRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7011",
                    audience: "https://localhost:7011",
                    claims: claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return HttpResponse.OK(new AuthenticatedResponse { Token = tokenString });
            }
        }
    }
}
