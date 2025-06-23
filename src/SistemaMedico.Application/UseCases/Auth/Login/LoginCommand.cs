namespace SistemaMedico.Application.UseCases.Auth.Login;

public class LoginCommand
{
    public string Email { get; init; }
    public string Password { get; init; } // en texto plano (se validará contra hash)
}
