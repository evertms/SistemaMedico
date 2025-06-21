using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface ISpecialtyRepository : IRepository<Specialty>
{
    Task<bool> ExistsByIdAsync(Guid specialtyId);
}