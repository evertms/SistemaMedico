using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<bool> HasPendingReminderForAppointment(Guid appointmentId);
    Task<IEnumerable<Notification>> GetUpcomingReminders();
}