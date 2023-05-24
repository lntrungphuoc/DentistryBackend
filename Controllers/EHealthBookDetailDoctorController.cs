using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.EHealthBookDetailDoctor;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("eHealthBookDetailDoctor"), Authorize(Roles = AppConstants.ApplicationRoles.HEALTHBOOK)]
    public class EHealthBookDetailDoctorController : Controller
    {
        private readonly IEHealthBookDetailDoctorServices _services;

        public EHealthBookDetailDoctorController(IEHealthBookDetailDoctorServices services)
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
        public IActionResult Create(EHealthBookDetailDoctorRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(EHealthBookDetailDoctorRequest request)
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
