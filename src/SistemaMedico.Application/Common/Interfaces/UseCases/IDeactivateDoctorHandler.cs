using SistemaMedico.Application.UseCases.Doctors.DeactivateDoctor;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IDeactivateDoctorHandler
{
    Task HandleAsync(DeactivateDoctorCommand command);
}