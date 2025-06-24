using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class MedicalRecordRepository 
    : RepositoryBase<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<MedicalRecord?> GetMedicalRecordByPatientId(Guid patientId)
    {
        return await _dbSet
            .Include(r => r.Notes)
            .Include(r => r.Diagnoses)
            .FirstOrDefaultAsync(r => r.PatientId == patientId);
    }

    public async Task<bool> IsNoteFromPatient(Guid medicalRecordId, Guid noteId, Guid patientId)
    {
        return await _dbSet
            .Where(r => r.Id == medicalRecordId && r.PatientId == patientId)
            .SelectMany(r => r.Notes)
            .AnyAsync(n => n.Id == noteId);
    }

    public async Task<bool> IsDiagnosisLinkedToNote(Guid diagnosisId, Guid noteId)
    {
        return await _context.MedicalNotes
            .AnyAsync(n => n.Id == noteId && n.DiagnosisId == diagnosisId);
    }
}