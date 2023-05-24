using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("service")]
    public class ServiceController : Controller
    {
        private readonly IServiceServices _services;

        public ServiceController(IServiceServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var res = _services.GetAll();
            return Ok(res);
        }

        [HttpGet("{url}")]
        public IActionResult GetByURL(string url)
        {
            var res = _services.GetByURL(url);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create(ServiceRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(ServiceRequest request)
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

        [HttpPost("calculate")]
        public IActionResult CalculateFee([FromBody] List<ServiceRequest> requests)
        {
            var res = _services.CalculateFee(requests);
            return Ok(res);
        }
    }
}
