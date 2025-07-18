using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetByUserIdAsync(Guid userId);
    Task<bool> HasSpecialty(Guid doctorId, Guid specialtyId);
    Task<bool> IsValidLicense(Guid doctorId, string licenseNumber);
}