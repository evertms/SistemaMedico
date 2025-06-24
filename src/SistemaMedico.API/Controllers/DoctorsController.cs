using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Doctors.AddDoctor;
using SistemaMedico.Application.UseCases.Doctors.DeactivateDoctor;
using SistemaMedico.Application.UseCases.Doctors.EditDoctor;
using SistemaMedico.Application.UseCases.Doctors.GetAvailableDoctors;
using SistemaMedico.Application.UseCases.Doctors.GetAvailableSchedules;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IAddDoctorHandler _addHandler;
    private readonly IEditDoctorHandler _editHandler;
    private readonly IDeactivateDoctorHandler _deactivateHandler;
    private readonly IGetAvailableDoctorsHandler _getAvailableDoctorsHandler;
    private readonly IGetAvailableSchedulesHandler _getAvailableSchedulesHandler;

    public DoctorsController(
        IAddDoctorHandler addHandler,
        IEditDoctorHandler editHandler,
        IDeactivateDoctorHandler deactivateHandler,
        IGetAvailableDoctorsHandler getAvailableDoctorsHandler,
        IGetAvailableSchedulesHandler getAvailableSchedulesHandler)
    {
        _addHandler = addHandler;
        _editHandler = editHandler;
        _deactivateHandler = deactivateHandler;
        _getAvailableDoctorsHandler = getAvailableDoctorsHandler;
        _getAvailableSchedulesHandler = getAvailableSchedulesHandler;
    }

    /// <summary>
    /// Agregar un nuevo médico (Administrador)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Add([FromBody] AddDoctorCommand command)
    {
        var result = await _addHandler.HandleAsync(command);
        return Ok(result);
    }

    /// <summary>
    /// Editar información de un médico (Administrador)
    /// </summary>
    [HttpPut("{doctorId:guid}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Edit(Guid doctorId, [FromBody] EditDoctorCommand command)
    {
        var newCommand = new EditDoctorCommand
        {
            DoctorId = doctorId,
            NewLicenseNumber = command.NewLicenseNumber,
            NewSpecialtyId = command.NewSpecialtyId
        };
        await _editHandler.HandleAsync(newCommand);
        return NoContent();
    }

    /// <summary>
    /// Dar de baja a un médico (Administrador)
    /// </summary>
    [HttpDelete("{doctorId:guid}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Deactivate(Guid doctorId)
    {
        var command = new DeactivateDoctorCommand { DoctorId = doctorId };
        await _deactivateHandler.HandleAsync(command);
        return NoContent();
    }

    /// <summary>
    /// Obtener médicos disponibles (Paciente)
    /// </summary>
    [HttpGet("available")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailableDoctors()
    {
        var result = await _getAvailableDoctorsHandler.HandleAsync(new GetAvailableDoctorsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Obtener horarios disponibles de un médico (Paciente)
    /// </summary>
    [HttpGet("{doctorId:guid}/schedules")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailableSchedules(Guid doctorId)
    {
        var query = new GetAvailableSchedulesQuery { DoctorId = doctorId };
        var result = await _getAvailableSchedulesHandler.HandleAsync(query);
        return Ok(result);
    }
}
