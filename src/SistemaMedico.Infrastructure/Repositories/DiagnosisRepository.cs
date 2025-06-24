using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class DiagnosisRepository 
    : RepositoryBase<Diagnosis>, IDiagnosisRepository
{
    public DiagnosisRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<bool> HasMedicalNote(Guid diagnosisId)
    {
        return await _dbSet
            .AnyAsync(d => d.Id == diagnosisId && d.MedicalNoteId != null);
    }

    public async Task<bool> IsConfirmed(Guid diagnosisId)
    {
        return await _dbSet
            .AnyAsync(d => d.Id == diagnosisId && d.IsConfirmed);
    }
}