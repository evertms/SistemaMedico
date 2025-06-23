using SistemaMedico.Application.UseCases.Notifications.SendAppointmentReminder;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface ISendAppointmentReminderHandler
{
    Task HandleAsync(SendAppointmentReminderCommand command);
}