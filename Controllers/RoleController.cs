using AppDentistry.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppDentistry.Controllers
{
    [ApiController, Route("role")]
    public class RoleController : Controller
    {
        private IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet("getAll")]
        public IActionResult Index()
        {
            var res = _roleServices.GetAll();
            return Ok(res);
        }
    }
}
