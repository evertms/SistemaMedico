using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Appointments.CancelAppointment;
using SistemaMedico.Application.UseCases.Appointments.GetDoctorAppointments;
using SistemaMedico.Application.UseCases.Appointments.RescheduleAppointment;
using SistemaMedico.Application.UseCases.Appointments.ScheduleAppointment;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IScheduleAppointmentHandler _scheduleHandler;
    private readonly ICancelAppointmentHandler _cancelHandler;
    private readonly IRescheduleAppointmentHandler _rescheduleHandler;
    private readonly IGetDoctorAppointmentsHandler _getAppointmentsHandler;

    public AppointmentsController(
        IScheduleAppointmentHandler scheduleHandler,
        ICancelAppointmentHandler cancelHandler,
        IRescheduleAppointmentHandler rescheduleHandler,
        IGetDoctorAppointmentsHandler getAppointmentsHandler)
    {
        _scheduleHandler = scheduleHandler;
        _cancelHandler = cancelHandler;
        _rescheduleHandler = rescheduleHandler;
        _getAppointmentsHandler = getAppointmentsHandler;
    }

    /// <summary>
    /// Programar una nueva consulta médica
    /// </summary>
    [HttpPost("schedule")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Schedule([FromBody] ScheduleAppointmentCommand command)
    {
        var result = await _scheduleHandler.HandleAsync(command);
        return Ok(result);
    }

    /// <summary>
    /// Cancelar una cita médica
    /// </summary>
    [HttpPost("cancel")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Cancel([FromBody] CancelAppointmentCommand command)
    {
        await _cancelHandler.HandleAsync(command);
        return NoContent();
    }

    /// <summary>
    /// Reprogramar una cita médica
    /// </summary>
    [HttpPost("reschedule")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Reschedule([FromBody] RescheduleAppointmentCommand command)
    {
        await _rescheduleHandler.HandleAsync(command);
        return NoContent();
    }

    /// <summary>
    /// Obtener todas las citas de un médico
    /// </summary>
    [HttpGet("doctor/{doctorId:guid}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetByDoctor(Guid doctorId)
    {
        var query = new GetDoctorAppointmentsQuery{DoctorId = doctorId};
        var result = await _getAppointmentsHandler.HandleAsync(query);
        return Ok(result);
    }
}
