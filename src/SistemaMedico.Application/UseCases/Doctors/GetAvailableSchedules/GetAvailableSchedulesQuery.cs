namespace SistemaMedico.Application.UseCases.Doctors.GetAvailableSchedules;

public class GetAvailableSchedulesQuery
{
    public Guid DoctorId { get; init; }
    public DateTime? Date { get; init; }
}
