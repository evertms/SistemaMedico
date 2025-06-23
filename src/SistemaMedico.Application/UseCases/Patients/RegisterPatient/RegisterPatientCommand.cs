namespace SistemaMedico.Application.UseCases.Patients.RegisterPatient;

public class RegisterPatientCommand
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string PhoneNumber { get; init; }
}
