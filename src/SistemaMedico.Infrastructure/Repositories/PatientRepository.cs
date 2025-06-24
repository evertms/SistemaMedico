using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class PatientRepository 
    : RepositoryBase<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<bool> HasActiveAppointment(Guid patientId)
    {
        return await _context.MedicalAppointments
            .AnyAsync(a =>
                a.PatientId == patientId &&
                (a.Status == AppointmentStatus.Pending || a.Status == AppointmentStatus.Confirmed));
    }

    public async Task<bool> ExistsByIdAsync(Guid patientId)
    {
        return await _dbSet.AnyAsync(p => p.Id == patientId);
    }

    public async Task<bool> ExistsWithEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Patients)
            .AnyAsync(u =>
                u.Email == email &&
                u.Patients.Any());
    }
}