using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Users.RegisterUser;

public class RegisterUserHandler : IRegisterUserHandler
{
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> HandleAsync(RegisterUserCommand command)
    {
        // Validar si ya existe usuario con ese email
        var existing = await _userRepository.GetByEmailAsync(command.Email);
        if (existing is not null)
            throw new DomainException("Ya existe un usuario con ese correo.");

        var user = new User(
            email: command.Email,
            passwordHash: command.PasswordHash,
            phoneNumber: command.PhoneNumber,
            role: command.Role
        );

        await _userRepository.AddAsync(user);
        return user.Id;
    }
}
