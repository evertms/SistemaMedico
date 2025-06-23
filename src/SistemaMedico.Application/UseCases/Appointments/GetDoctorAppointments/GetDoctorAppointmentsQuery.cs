namespace SistemaMedico.Application.UseCases.Appointments.GetDoctorAppointments;

public class GetDoctorAppointmentsQuery
{
    public Guid DoctorId { get; init; }
    public DateTime? Date { get; init; } // opcional para filtrar
}
