using SistemaMedico.Application.UseCases.Doctors.AddDoctor;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IAddDoctorHandler
{
    Task<Guid> HandleAsync(AddDoctorCommand command);
}