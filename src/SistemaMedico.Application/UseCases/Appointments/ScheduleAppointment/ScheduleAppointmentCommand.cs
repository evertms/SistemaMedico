namespace SistemaMedico.Application.UseCases.Appointments.ScheduleAppointment;

public class ScheduleAppointmentCommand
{
    public Guid PatientId { get; init; }
    public Guid DoctorId { get; init; }
    public Guid ScheduleId { get; init; }
    public TimeSpan Duration { get; init; }
    public string? ReasonForVisit { get; init; }
}
