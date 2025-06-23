namespace SistemaMedico.Application.UseCases.Specialties.GetDoctorsBySpecialty;

public class GetDoctorsBySpecialtyQuery
{
    public Guid SpecialtyId { get; init; }
    public DateTime? Date { get; init; }
}
