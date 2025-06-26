using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Appointments.CancelAppointment;
using SistemaMedico.Application.UseCases.Appointments.GetDoctorAppointments;
using SistemaMedico.Application.UseCases.Appointments.GetMyPendingAppointments;
using SistemaMedico.Application.UseCases.Appointments.RescheduleAppointment;
using SistemaMedico.Application.UseCases.Appointments.ScheduleAppointment;
using SistemaMedico.Application.Common.Extensions;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IScheduleAppointmentHandler _scheduleHandler;
    private readonly ICancelAppointmentHandler _cancelHandler;
    private readonly IRescheduleAppointmentHandler _rescheduleHandler;
    private readonly IGetDoctorAppointmentsHandler _getAppointmentsHandler;
    private readonly IGetMyPendingAppointmentsHandler _getMyPendingAppointmentsHandler;

    public AppointmentsController(
        IScheduleAppointmentHandler scheduleHandler,
        ICancelAppointmentHandler cancelHandler,
        IRescheduleAppointmentHandler rescheduleHandler,
        IGetDoctorAppointmentsHandler getAppointmentsHandler,
        IGetMyPendingAppointmentsHandler getMyPendingAppointmentsHandler)
    {
        _scheduleHandler = scheduleHandler;
        _cancelHandler = cancelHandler;
        _rescheduleHandler = rescheduleHandler;
        _getAppointmentsHandler = getAppointmentsHandler;
        _getMyPendingAppointmentsHandler = getMyPendingAppointmentsHandler;
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
    
    [HttpGet("my-pending")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetMyPendingAppointments()
    {
        Console.WriteLine(User.Identity?.IsAuthenticated);
        Console.WriteLine("ENTRÓ AL CONTROLADOR");
        try
        {
            var patientId = User.GetPatientId();
            var query = new GetMyPendingAppointmentsQuery(patientId);
            var result = await _getMyPendingAppointmentsHandler.HandleAsync(query);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
