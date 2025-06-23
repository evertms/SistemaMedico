namespace SistemaMedico.Application.DTOs;

public class ScheduleDto
{
    public Guid ScheduleId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan Duration { get; set; }
}
