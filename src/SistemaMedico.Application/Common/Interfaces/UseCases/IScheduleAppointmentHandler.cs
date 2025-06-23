using SistemaMedico.Application.UseCases.Appointments.ScheduleAppointment;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IScheduleAppointmentHandler
{
    Task<Guid> HandleAsync(ScheduleAppointmentCommand command);
}