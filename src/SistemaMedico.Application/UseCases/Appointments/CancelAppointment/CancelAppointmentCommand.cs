namespace SistemaMedico.Application.UseCases.Appointments.CancelAppointment;

public class CancelAppointmentCommand
{
    public Guid AppointmentId { get; init; }
    public Guid PatientId { get; init; } // Para validar que la cita le pertenece
}
