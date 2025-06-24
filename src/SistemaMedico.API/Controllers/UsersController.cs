using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Users.RegisterUser;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRegisterUserHandler _registerUserHandler;

    public UsersController(IRegisterUserHandler registerUserHandler)
    {
        _registerUserHandler = registerUserHandler;
    }

    // POST: api/users/register
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        await _registerUserHandler.HandleAsync(command);
        return NoContent();
    }
}