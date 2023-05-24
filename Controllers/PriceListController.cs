using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.PriceList;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("price-list")]
    public class PriceListController : Controller
    {
        private readonly IPriceListServices _services;

        public PriceListController(IPriceListServices services)
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
        public IActionResult Create(PriceListRequest request)
        {
            request.CreatedBy = (int)new Identity(HttpContext.User).Id!;
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update(PriceListRequest request)
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
