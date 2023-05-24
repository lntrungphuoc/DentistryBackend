using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Doctor;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("doctor")]
    public class DoctorController : Controller
    {
        private IDoctorServices _services;

        public DoctorController(IDoctorServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var res = _services.GetAll();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _services.GetById(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create(DoctorRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(DoctorRequest request)
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
