using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppDentistry.Controllers
{
    [ApiController, Route("user")]
    public class UserController : Controller
    {
        private readonly IUserServices _services;

        public UserController(IUserServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _services.GetAll();
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var res = await _services.LoginAsync(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res.Data) : BadRequest(res);
        }

        [HttpGet("getLoggedInfo")]
        public IActionResult GetLoggedInfo(string userName)
        {
            var res = _services.GetLoggedInfo(userName);
            return Ok(res);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserRequest request)
        {
            var res = await _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserRequest request)
        {
            var res = await _services.Update(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }
    }
}
