using SistemaMedico.Application.UseCases.Schedules.CreateDoctorAvailability;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface ICreateDoctorAvailabilityHandler
{
    Task<Guid> HandleAsync(CreateDoctorAvailabilityCommand command);
}