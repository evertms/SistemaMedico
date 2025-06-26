namespace SistemaMedico.Application.DTOs;

public class AppointmentDto
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string Status { get; set; }
    public string? ReasonForVisit { get; set; }
}
