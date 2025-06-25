using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SistemaMedico.Application.UseCases.Admin.RegisterAdmin;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly RegisterAdminHandler _handler;
    private readonly IHostEnvironment _env;

    public AdminController(RegisterAdminHandler handler, IHostEnvironment env)
    {
        _handler = handler;
        _env = env;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminCommand command)
    {
        if (!_env.IsDevelopment())
            return Forbid("Solo disponible en desarrollo.");

        var adminId = await _handler.HandleAsync(command);
        return CreatedAtAction(null, new { id = adminId });
    }
}
