using SistemaMedico.Application.UseCases.Auth.Login;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface ILoginHandler
{
    Task<LoginResponse> HandleAsync(LoginCommand command);
}
