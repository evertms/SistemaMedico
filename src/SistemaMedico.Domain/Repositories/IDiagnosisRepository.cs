using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IDiagnosisRepository : IRepository<Diagnosis>
{
    Task<bool> HasMedicalNote(Guid diagnosisId);
    Task<bool> IsConfirmed(Guid diagnosisId);
}