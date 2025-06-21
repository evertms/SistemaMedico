namespace SistemaMedico.Domain.Helpers;

public static class DomainValidation
{
    public static void EnsureNotNull(Object paramValue, string paramName)
    {
        if (paramValue == null)
            throw new ArgumentException($"{paramName} no puede ser nulo", paramName);
    }
    public static void EnsureNotNullOrEmpty(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} no puede estar vacío", paramName);
    }

    public static void EnsureValidEmail(string email, string paramName)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException("El correo electrónico no es válido", paramName);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static void EnsureNotFutureDate(DateTime date, string paramName)
    {
        if (date > DateTime.Now)
            throw new ArgumentException($"{paramName} no puede ser una fecha futura", paramName);
    }

    public static void EnsureNotPastDate(DateTime date, string paramName)
    {
        if (date < DateTime.Now)
            throw new ArgumentException($"{paramName} no puede ser una fecha pasada", paramName);
    }

    public static void EnsureNotZeroDuration(TimeSpan duration, string paramName)
    {
        if (duration <= TimeSpan.Zero)
            throw new ArgumentException($"La duración tiene que ser mayor que cero", paramName);

    }

    public static void EnsureNotEmptyGuid(Guid guid, string paramName)
    {
        if (guid == Guid.Empty)
            throw new ArgumentException($"{paramName} no puede estar vacío", paramName);
    }

    public static void ValidateDoctorSchedule(Guid doctorId, DateTime startDate, TimeSpan duration)
    {
        if (doctorId == Guid.Empty)
            throw new ArgumentException("El ID del médico no puede estar vacío", nameof(doctorId));

        if (startDate < DateTime.UtcNow)
            throw new ArgumentException("No se pueden crear horarios pasados", nameof(startDate));

        if (duration <= TimeSpan.Zero)
            throw new ArgumentException("La duración debe ser mayor a cero", nameof(duration));
    }
    
    public static void ValidateNotification(Guid userId, string title, string message, DateTime scheduledAt)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("El usuario no puede estar vacío", nameof(userId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("El título no puede estar vacío", nameof(title));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("El mensaje no puede estar vacío", nameof(message));

        if (scheduledAt < DateTime.UtcNow)
            throw new ArgumentException("La fecha programada no puede ser pasada", nameof(scheduledAt));
    }
}