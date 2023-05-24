using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Customer;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerServices _services;

        public CustomerController(ICustomerServices services)
        {
            _services = services;
        }

        [HttpGet, Authorize(Roles = AppConstants.ApplicationRoles.CUSTOMER + "," + AppConstants.ApplicationRoles.HEALTHBOOK)]
        public IActionResult Index()
        {
            var res = _services.GetAll();
            return Ok(res);
        }

        [HttpPost("login"), AllowAnonymous]
        public IActionResult Login(LoginRequest request)
        {
            var res = _services.Login(request.PhoneNumber, request.Password);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : Ok(res);
        }

        [HttpPost("create"), Authorize(Roles = AppConstants.ApplicationRoles.CUSTOMER)]
        public IActionResult Create(CustomerRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update"), Authorize(Roles = AppConstants.ApplicationRoles.CUSTOMER)]
        public IActionResult Update(CustomerRequest request)
        {
            request.ModifiedBy = (int?)new Identity(HttpContext.User).Id;
            var res = _services.Update(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpDelete("delete"), Authorize(Roles = AppConstants.ApplicationRoles.CUSTOMER)]
        public IActionResult Delete(int id)
        {
            var res = _services.Delete(id);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }
    }
}
