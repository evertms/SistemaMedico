using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Infrastructure.Repositories;

public class DiagnosisRepository : IDiagnosisRepository
{
    public Task<IEnumerable<Diagnosis>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Diagnosis> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Diagnosis entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Diagnosis entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasMedicalNote(Guid diagnosisId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsConfirmed(Guid diagnosisId)
    {
        throw new NotImplementedException();
    }
}