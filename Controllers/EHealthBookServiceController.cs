using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookService;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("eHealthBookService"), Authorize(Roles = AppConstants.ApplicationRoles.HEALTHBOOK)]
    public class EHealthBookServiceController : Controller
    {
        private readonly IEHealthBookServiceServices _services;

        public EHealthBookServiceController(IEHealthBookServiceServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetByEHealthBookId(int id)
        {
            var res = _services.GetByEHealthBookId(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create(EHealthBookServiceRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(EHealthBookServiceRequest request)
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

        [HttpDelete("deleteRange")]
        public IActionResult DeleteRange(int idEHealthBook)
        {
            var res = _services.DeleteRange(idEHealthBook);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }
    }
}
