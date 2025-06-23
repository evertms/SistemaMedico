namespace SistemaMedico.Application.UseCases.Patients.UnlinkPatient;

public class UnlinkPatientCommand
{
    public Guid UserId { get; init; }
    public Guid PatientId { get; init; }
}
