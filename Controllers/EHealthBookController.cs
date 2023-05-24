using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBook;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("eHealthBook"), Authorize(Roles = AppConstants.ApplicationRoles.HEALTHBOOK)]
    public class EHealthBookController : Controller
    {
        private IEHealthBookServices _services;

        public EHealthBookController(IEHealthBookServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _services.GetAll();
            return Ok(res);
        }

        [HttpGet("{id}"), AllowAnonymous]
        public IActionResult GetByIdUser(int id)
        {
            var res = _services.GetByIdUser(id);
            return Ok(res);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var res = _services.GetById(id);
            return Ok(res);
        }

        [HttpGet("newest")]
        public IActionResult GetNewest()
        {
            var res = _services.GetNewest();
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create(EHealthBookRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(EHealthBookRequest request)
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
