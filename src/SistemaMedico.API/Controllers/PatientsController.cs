using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Patients.RegisterPatient;
using SistemaMedico.Application.UseCases.Patients.UnlinkPatient;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IRegisterPatientHandler _registerPatientHandler;
    private readonly IUnlinkPatientHandler _unlinkPatientHandler;

    public PatientsController(
        IRegisterPatientHandler registerPatientHandler,
        IUnlinkPatientHandler unlinkPatientHandler)
    {
        _registerPatientHandler = registerPatientHandler;
        _unlinkPatientHandler = unlinkPatientHandler;
    }

    // POST: api/patients/register
    [HttpPost("register")]
    public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientCommand command)
    {
        await _registerPatientHandler.HandleAsync(command);
        return NoContent();
    }

    // DELETE: api/patients/{patientId}/unlink
    [HttpDelete("{patientId}/unlink")]
    public async Task<IActionResult> UnlinkPatient(Guid patientId)
    {
        var command = new UnlinkPatientCommand
        {
            PatientId = patientId
        };

        await _unlinkPatientHandler.HandleAsync(command);
        return NoContent();
    }
}