using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class MedicalNoteRepository 
    : RepositoryBase<MedicalNote>, IMedicalNoteRepository
{
    public MedicalNoteRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<bool> BelongsToPatient(Guid noteId, Guid patientId)
    {
        return await _dbSet.AnyAsync(n =>
            n.Id == noteId &&
            n.PatientId == patientId);
    }

    public async Task<IEnumerable<MedicalNote>> GetNotesByAppointment(Guid appointmentId)
    {
        return await _dbSet
            .Where(n => n.AppointmentId == appointmentId)
            .ToListAsync();
    }
}