using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.News;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("news")]
    public class NewsController : Controller
    {
        private readonly INewsServices _services;

        public NewsController(INewsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var res = _services.GetAll();
            return Ok(res);
        }

        [HttpGet("for-web")]
        public IActionResult GetForWeb()
        {
            var res = _services.GetForWeb();
            return Ok(res);
        }

        [HttpGet("{url}")]
        public IActionResult GetByURL(string url)
        {
            var res = _services.GetByURL(url);
            return Ok(res);
        }

        [HttpGet("top3newest")]
        public IActionResult GetTop3Newest()
        {
            var res = _services.GetTop3Newest();
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create(NewsRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(NewsRequest request)
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
