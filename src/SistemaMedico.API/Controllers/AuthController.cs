using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Auth.Login;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginHandler _loginHandler;

    public AuthController(ILoginHandler loginHandler)
    {
        _loginHandler = loginHandler;
    }

    /// <summary>
    /// Iniciar sesión y obtener token
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var response = await _loginHandler.HandleAsync(command);
        return Ok(response);
    }
}