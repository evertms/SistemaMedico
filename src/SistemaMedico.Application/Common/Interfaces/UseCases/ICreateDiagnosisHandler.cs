using SistemaMedico.Application.UseCases.MedicalRecords.CreateDiagnosis;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface ICreateDiagnosisHandler
{
    Task<Guid> HandleAsync(CreateDiagnosisCommand command);
}