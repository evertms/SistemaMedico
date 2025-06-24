using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class NotificationRepository 
    : RepositoryBase<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<bool> HasPendingReminderForAppointment(Guid appointmentId)
    {
        return await _dbSet.AnyAsync(n =>
            n.AppointmentId == appointmentId &&
            n.SentAt != null &&
            !n.IsRead &&
            n.ScheduledAt <= DateTime.UtcNow);
    }

    public async Task<IEnumerable<Notification>> GetUpcomingReminders()
    {
        var now = DateTime.UtcNow;

        return await _dbSet
            .Where(n => n.SentAt == null && n.ScheduledAt <= now)
            .ToListAsync();
    }
}