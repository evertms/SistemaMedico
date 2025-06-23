using SistemaMedico.Application.UseCases.Doctors.EditDoctor;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IEditDoctorHandler
{
    Task HandleAsync(EditDoctorCommand command);
}