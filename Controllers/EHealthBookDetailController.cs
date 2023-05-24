using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookDetail;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("eHealthBookDetail"), Authorize(Roles = AppConstants.ApplicationRoles.HEALTHBOOK)]
    public class EHealthBookDetailController : Controller
    {
        private readonly IEHealthBookDetailServices _services;

        public EHealthBookDetailController(IEHealthBookDetailServices services)
        {
            this._services = services;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public IActionResult Index(int id)
        {
            var res = _services.GetByIdHealthBook(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create(EHealthBookDetailRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(EHealthBookDetailRequest request)
        {
            request.ModifiedBy = (int?)new Identity(HttpContext.User).Id;
            var res = _services.Update(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var res = _services.Delete(id);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }
    }
}
