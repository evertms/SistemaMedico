using SistemaMedico.Application.UseCases.Appointments.RescheduleAppointment;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IRescheduleAppointmentHandler
{
    Task HandleAsync(RescheduleAppointmentCommand command);
}