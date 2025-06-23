using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Application.UseCases.Users.RegisterUser;

public class RegisterUserCommand
{
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public string PhoneNumber { get; init; }
    public UserRole Role { get; init; } = UserRole.Patient; // por defecto paciente
}
