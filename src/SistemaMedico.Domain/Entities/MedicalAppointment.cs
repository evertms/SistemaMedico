using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class MedicalAppointment
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid ScheduleId { get; private set; }

    public DateTime ScheduledDate { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public string? ReasonForVisit { get; private set; }
    public TimeSpan Duration { get; private set; }

    // Relaciones
    public Patient Patient { get; private set; }
    public Doctor? Doctor { get; private set; }
    public AvailableDoctorSchedule Schedule { get; private set; }
    public Guid? MedicalNoteId { get; private set; }
    public MedicalNote? MedicalNote { get; private set; }

    public MedicalAppointment() { }

    public MedicalAppointment(
        Guid patientId,
        Guid doctorId,
        Guid scheduleId,
        DateTime scheduledDate,
        TimeSpan duration,
        string? reasonForVisit = null)
    {
        Validate(scheduledDate, duration);

        Id = Guid.NewGuid();
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduleId = scheduleId;
        ScheduledDate = scheduledDate;
        Duration = duration;
        Status = AppointmentStatus.Pending;
        ReasonForVisit = reasonForVisit;
    }

    private void Validate(DateTime date, TimeSpan duration)
    {
        DomainValidation.EnsureNotPastDate(date, nameof(date));
        DomainValidation.EnsureNotZeroDuration(duration, nameof(duration));
    }

    public void Confirm()
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new DomainException("No se puede confirmar una cita cancelada.");

        if (Status != AppointmentStatus.Pending)
            throw new DomainException("Solo se pueden confirmar citas en estado pendiente.");

        Status = AppointmentStatus.Confirmed;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.Confirmed)
            throw new DomainException("Solo se pueden completar citas confirmadas.");

        Status = AppointmentStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Completed)
            throw new DomainException("No se puede cancelar una cita ya realizada.");

        Status = AppointmentStatus.Cancelled;
    }

    public void Reschedule(DateTime newDate, Guid newScheduleId)
    {
        if (Status != AppointmentStatus.Pending && Status != AppointmentStatus.Confirmed)
            throw new DomainException("Solo se pueden reprogramar citas pendientes o confirmadas.");

        DomainValidation.EnsureNotPastDate(newDate, nameof(newDate));
        ScheduledDate = newDate;
        ScheduleId = newScheduleId;
        Status = AppointmentStatus.Pending;
    }

    public void LinkSchedule(AvailableDoctorSchedule schedule)
    {
        if (schedule == null)
            throw new DomainException("Debe especificarse un horario disponible.");

        if (!schedule.IsBooked)
            throw new DomainException("El horario seleccionado ya está ocupado.");

        Schedule = schedule;
        Schedule.MarkAsBooked();
    }

    public void EnsureDoctorHasCorrectSpecialty(Guid expectedSpecialtyId)
    {
        if (Doctor is null)
            throw new DomainException("La cita no tiene un médico cargado.");

        if (Doctor.SpecialtyId != expectedSpecialtyId)
            throw new DomainException("El médico no tiene la especialidad requerida para esta cita.");
    }

    public void EnforceValidStateTransition(AppointmentStatus nextStatus)
    {
        var validTransitions = new Dictionary<AppointmentStatus, AppointmentStatus[]>
        {
            { AppointmentStatus.Pending, new[] { AppointmentStatus.Confirmed, AppointmentStatus.Cancelled } },
            { AppointmentStatus.Confirmed, new[] { AppointmentStatus.Completed, AppointmentStatus.Cancelled } },
        };

        if (!validTransitions.TryGetValue(Status, out var validNext) || !validNext.Contains(nextStatus))
            throw new DomainException($"No se puede cambiar el estado de {Status} a {nextStatus}.");

        Status = nextStatus;
    }
}

public enum AppointmentStatus
{
    Pending,
    Confirmed,
    Completed,
    Cancelled
}