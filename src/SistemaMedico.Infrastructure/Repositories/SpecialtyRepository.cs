using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class SpecialtyRepository 
    : RepositoryBase<Specialty>, ISpecialtyRepository
{
    public SpecialtyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<bool> ExistsByIdAsync(Guid specialtyId)
    {
        return _dbSet
            .AnyAsync(s => s.Id == specialtyId);
    }
}