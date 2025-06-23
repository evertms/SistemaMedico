using SistemaMedico.Application.UseCases.Appointments.CancelAppointment;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface ICancelAppointmentHandler
{
    Task HandleAsync(CancelAppointmentCommand command);
}