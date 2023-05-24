using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookDetailService;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("eHealthBookDetailService"), Authorize(Roles = AppConstants.ApplicationRoles.HEALTHBOOK)]
    public class EHealthBookDetailServiceController : Controller
    {
        private readonly IEHealthBookDetailServiceServices _services;

        public EHealthBookDetailServiceController(IEHealthBookDetailServiceServices services)
        {
            _services = services;
        }

        [HttpGet("{id}")]
        public IActionResult GetByEHealthBookDetailId(int id)
        {
            var res = _services.GetByEHealthBookDetailId(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create(EHealthBookDetailServiceRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(EHealthBookDetailServiceRequest request)
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
        public IActionResult DeleteRange(int id)
        {
            var res = _services.DeleteRange(id);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }
    }
}
