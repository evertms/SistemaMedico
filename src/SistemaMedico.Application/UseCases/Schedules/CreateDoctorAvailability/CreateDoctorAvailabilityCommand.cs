namespace SistemaMedico.Application.UseCases.Schedules.CreateDoctorAvailability;

public class CreateDoctorAvailabilityCommand
{
    public Guid DoctorId { get; init; }
    public DateTime StartDate { get; init; }
    public TimeSpan Duration { get; init; }
}
