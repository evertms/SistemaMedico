using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class AvailableDoctorSchedule
{
    public Guid Id { get; private set; }
    public Guid DoctorId { get; private set; }

    public DateTime StartDate { get; private set; }
    public TimeSpan Duration { get; private set; } = TimeSpan.FromMinutes(30);

    public bool IsBooked { get; private set; }

    public Doctor? Doctor { get; private set; }
    private readonly List<MedicalAppointment> _appointments = new();
    public IReadOnlyCollection<MedicalAppointment> Appointments => _appointments.AsReadOnly();

    public AvailableDoctorSchedule() { } // Para usar con EF Core

    public AvailableDoctorSchedule(
        Guid doctorId,
        DateTime startDate,
        TimeSpan duration)
    {
        Validate(doctorId, startDate, duration);

        Id = Guid.NewGuid();
        DoctorId = doctorId;
        StartDate = startDate;
        Duration = duration;
        IsBooked = false;
    }

    private void Validate(Guid doctorId, DateTime startDate, TimeSpan duration)
    {
        DomainValidation.EnsureNotEmptyGuid(doctorId, nameof(doctorId));
        DomainValidation.EnsureNotPastDate(startDate, nameof(startDate));
        DomainValidation.EnsureNotZeroDuration(duration, nameof(duration));
    }

    public void MarkAsBooked()
    {
        if (IsBooked)
            throw new DomainException("Este horario ya está reservado.");

        IsBooked = true;
    }

    public void Release()
    {
        IsBooked = false;
    }

    public bool OverlapsWith(AvailableDoctorSchedule other)
    {
        if (other.DoctorId != DoctorId)
            return false;

        var thisEnd = StartDate + Duration;
        var otherEnd = other.StartDate + other.Duration;

        return StartDate < otherEnd && other.StartDate < thisEnd;
    }

    public bool ExceedsMaxAppointmentsPerDay(IEnumerable<AvailableDoctorSchedule> allSchedulesForDoctor, int maxPerDay)
    {
        var sameDayCount = allSchedulesForDoctor.Count(s =>
            s.DoctorId == DoctorId &&
            s.StartDate.Date == StartDate.Date &&
            s.Id != Id); // excluir el actual

        return sameDayCount + 1 > maxPerDay;
    }

    public void AddAppointment(MedicalAppointment appointment)
    {
        if (IsBooked)
            throw new DomainException("Este horario ya tiene una cita agendada.");

        if (appointment.ScheduledDate != StartDate)
            throw new DomainException("La cita no coincide con la hora de inicio del horario.");

        _appointments.Add(appointment);
        MarkAsBooked();
    }
}
