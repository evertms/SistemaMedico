namespace SistemaMedico.Application.UseCases.Doctors.GetAvailableDoctors;

public class GetAvailableDoctorsQuery
{
    public Guid? SpecialtyId { get; init; }
    public DateTime? Date { get; init; }
}
