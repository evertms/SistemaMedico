using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.UseCases.Schedules.CreateDoctorAvailability;
using System.Security.Claims;
using SistemaMedico.Application.Common.Interfaces.UseCases;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly ICreateDoctorAvailabilityHandler _createHandler;

    public SchedulesController(ICreateDoctorAvailabilityHandler createHandler)
    {
        _createHandler = createHandler;
    }

    // POST: api/schedules/availability
    [HttpPost("availability")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CreateDoctorAvailability([FromBody] CreateDoctorAvailabilityCommand command)
    {
        var doctorId = GetUserId();

        var newCommand = new CreateDoctorAvailabilityCommand
        {
            DoctorId = doctorId,
            StartDate = command.StartDate,
            Duration = command.Duration
        };

        await _createHandler.HandleAsync(newCommand);
        return NoContent();
    }

    private Guid GetUserId()
    {
        var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub");
        return claim is not null ? Guid.Parse(claim.Value) : Guid.Empty;
    }
}