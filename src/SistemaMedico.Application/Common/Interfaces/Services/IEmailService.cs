namespace SistemaMedico.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}
