using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.Attachment;
using Microsoft.AspNetCore.Mvc;
using static AppDentistry.Services.BaseService;

namespace AppDentistry.Controllers
{
    [ApiController, Route("attachment")]
    public class AttachmentController : Controller
    {
        private readonly IAttachmentServices _services;

        public AttachmentController(IAttachmentServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetByIdEHealthBookDetail(int id)
        {
            var res = _services.GetByEHealthBookDetail(id);
            return Ok(res);
        }

        [HttpPost("create")]
        public IActionResult Create([FromForm] ListAttachmentRequest request)
        {
            var res = _services.Create(request);
            return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateAttachmentRequest request)
        {
            var idUser = new Identity(HttpContext.User).Id;
            var res = _services.UpdateByEHealthBookDetail(request.listAttachment, (int)idUser, request.idEHealthBookDetail);
            return Ok(res);
        }
    }
}
