using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}