using AppDentistry.Interface.Services;
using AppDentistry.Models.Request.AppointmentSchedule;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json.Serialization;
using static AppDentistry.Services.BaseService;
using HttpResponse = AppDentistry.Common.Http.HttpResponse;

namespace AppDentistry.Controllers
{
    [ApiController, Route("appointment-schedule"), Authorize(Roles = AppConstants.ApplicationRoles.APPOINTMENT)]
    public class AppointmentScheduleController : Controller
    {
        private readonly IAppointmentScheduleServices _services;

        public AppointmentScheduleController(IAppointmentScheduleServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var appointmentSchedules = _services.GetAll();
            return Ok(appointmentSchedules);
        }

        [HttpPost("create"), AllowAnonymous]
        public IActionResult Create(AppointmentScheduleRequest request)
        {
            var idUser = new Identity(HttpContext.User).Id;
            if (idUser != null)
            {
                request.CreatedBy = (int)idUser;
            }
            var res = _services.Create(request);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] AppointmentScheduleRequest request)
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

        [HttpPut("confirm")]
        public IActionResult ConfirmAppointment(int id)
        {
            var res = _services.ConfirmAppointment(id);
            return res.StatusCode == (int)HttpStatusCode.OK ? Ok(res) : BadRequest(res);
        }

        [HttpGet("confirmed")]
        public IActionResult GetConfirmedAppointment()
        {
            var appointmentSchedules = _services.GetConfirmedAppointment();
            return Ok(appointmentSchedules);
        }

        [HttpGet("have-not-confirmed")]
        public IActionResult GetHaveNotConfirmedAppointment()
        {
            var appointmentSchedules = _services.GetHaveNotConfirmedAppointment();
            return Ok(appointmentSchedules);
        }
    }
}
