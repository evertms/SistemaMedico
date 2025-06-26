using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.Common.Interfaces.Services;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Application.UseCases.Auth.Login;

using SistemaMedico.Domain.Repositories;
using SistemaMedico.Application.Common.Interfaces;
using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Domain.Helpers;

public class LoginHandler : ILoginHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginHandler(
        IUserRepository userRepository,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
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

        Guid? doctorId = null;
        Guid? patientId = null;

        if (user.Role == UserRole.Doctor)
        {
            var doctor = await _doctorRepository.GetByUserIdAsync(user.Id);
            doctorId = doctor?.Id;
        }
        else if (user.Role == UserRole.Patient)
        {
            var patient = await _patientRepository.GetByUserIdAsync(user.Id);
            patientId = patient?.Id;
        }

        var token = _tokenService.GenerateToken(user, doctorId, patientId);

        return new LoginResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token,
            DoctorId = doctorId,
            PatientId = patientId
        };
    }

}
