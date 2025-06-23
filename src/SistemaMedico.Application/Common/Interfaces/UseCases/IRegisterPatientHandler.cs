using SistemaMedico.Application.UseCases.Patients.RegisterPatient;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IRegisterPatientHandler
{
    Task<Guid> HandleAsync(RegisterPatientCommand command);
}