using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<Doctor?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(d => d.UserId == userId);
    }
    
    public async Task<bool> HasSpecialty(Guid doctorId, Guid specialtyId)
    {
        return await _dbSet
            .AnyAsync(d => d.Id == doctorId && d.SpecialtyId == specialtyId);
    }

    public async Task<bool> IsValidLicense(Guid doctorId, string licenseNumber)
    {
        return await _dbSet
            .AnyAsync(d => d.Id == doctorId && d.LicenseNumber == licenseNumber);
    }
}