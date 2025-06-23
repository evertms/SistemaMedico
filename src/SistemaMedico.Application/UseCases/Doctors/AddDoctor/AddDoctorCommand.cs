namespace SistemaMedico.Application.UseCases.Doctors.AddDoctor;

public class AddDoctorCommand
{
    public Guid UserId { get; init; }
    public Guid SpecialtyId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string LicenseNumber { get; init; }
}
