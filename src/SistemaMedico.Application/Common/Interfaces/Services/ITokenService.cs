using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Application.Common.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
