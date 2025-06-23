namespace SistemaMedico.Application.UseCases.Doctors.EditDoctor;

public class EditDoctorCommand
{
    public Guid DoctorId { get; init; }
    public string NewLicenseNumber { get; init; }
    public Guid NewSpecialtyId { get; init; }
}
