using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.Common.Interfaces.Services;

namespace SistemaMedico.Application.UseCases.Auth.Login;

using SistemaMedico.Domain.Repositories;
using SistemaMedico.Application.Common.Interfaces;
using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Domain.Helpers;

public class LoginHandler : ILoginHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> HandleAsync(LoginCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
            throw new DomainException("Correo o contraseña incorrectos.");

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
            throw new DomainException("Correo o contraseña incorrectos.");

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token
        };
    }
}
