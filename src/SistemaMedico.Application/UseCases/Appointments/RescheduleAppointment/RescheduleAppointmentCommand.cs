namespace SistemaMedico.Application.UseCases.Appointments.RescheduleAppointment;

public class RescheduleAppointmentCommand
{
    public Guid AppointmentId { get; init; }
    public Guid PatientId { get; init; }
    public Guid NewScheduleId { get; init; }
    public DateTime NewDate { get; init; }
}
