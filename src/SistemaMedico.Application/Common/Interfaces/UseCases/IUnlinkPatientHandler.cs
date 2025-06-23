using SistemaMedico.Application.UseCases.Patients.UnlinkPatient;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IUnlinkPatientHandler
{
    Task HandleAsync(UnlinkPatientCommand command);
}