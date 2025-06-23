namespace SistemaMedico.Application.UseCases.MedicalRecords.CreateDiagnosis;

public class CreateDiagnosisCommand
{
    public Guid DoctorId { get; init; }
    public Guid PatientId { get; init; }
    public string Description { get; init; }
    public string? ICD10Code { get; init; }
    public bool IsConfirmed { get; init; }
    public Guid MedicalRecordId { get; init; }
    public Guid? MedicalNoteId { get; init; }
}
