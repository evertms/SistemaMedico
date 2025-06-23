namespace SistemaMedico.Application.DTOs;

public class DoctorAppointmentDto
{
    public Guid AppointmentId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public TimeSpan Duration { get; set; }
    public string Status { get; set; }
    public string PatientFullName { get; set; }
}
