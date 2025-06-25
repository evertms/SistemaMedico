using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Application.Common.Interfaces.Services;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Admin.RegisterAdmin;

public class RegisterAdminHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterAdminHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> HandleAsync(RegisterAdminCommand command)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email);
        if (existingUser != null)
            throw new DomainException("El usuario ya existe.");

        var passwordHash = _passwordHasher.Hash(command.Password);

        var user = new User(
            command.Email,
            passwordHash,
            command.PhoneNumber,
            UserRole.Administrador
            );

        await _userRepository.AddAsync(user);
        return user.Id;
    }
}
