using SistemaMedico.Application.UseCases.Users.RegisterUser;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IRegisterUserHandler
{
    Task<Guid> HandleAsync(RegisterUserCommand command);
}