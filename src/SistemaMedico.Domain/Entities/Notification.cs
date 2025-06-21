using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class Notification
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? AppointmentId { get; private set; }

    public string Title { get; private set; }
    public string Message { get; private set; }

    public bool IsRead { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public DateTime? SentAt { get; private set; }

    // Relaciones (opcional)
    public User? User { get; private set; }
    public MedicalAppointment? Appointment { get; private set; }

    protected Notification() { }

    public Notification(
        Guid userId,
        string title,
        string message,
        DateTime scheduledAt,
        Guid? appointmentId = null)
    {
        Validate(userId, title, message, scheduledAt, appointmentId);

        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Message = message;
        ScheduledAt = scheduledAt;
        AppointmentId = appointmentId;
        IsRead = false;
        SentAt = null;
    }

    private void Validate(Guid userId, string title, string message, DateTime scheduledAt, Guid? appointmentId)
    {
        DomainValidation.EnsureNotEmptyGuid(userId, nameof(userId));
        DomainValidation.EnsureNotNullOrEmpty(title, nameof(title));
        DomainValidation.EnsureNotNullOrEmpty(message, nameof(message));
        DomainValidation.EnsureNotPastDate(scheduledAt, nameof(scheduledAt));

        // Si es una notificación relacionada con una cita, debe tener appointmentId
        if (title.ToLower().Contains("cita") && appointmentId is null)
        {
            throw new DomainException("Las notificaciones relacionadas con citas deben tener un AppointmentId.");
        }
    }

    public void MarkAsRead()
    {
        if (!IsRead)
            IsRead = true;
    }

    public void MarkAsSent()
    {
        if (SentAt == null)
            SentAt = DateTime.UtcNow;
    }

    public bool IsPending =>
        !IsRead &&
        DateTime.UtcNow >= ScheduledAt &&
        SentAt != null;
}
