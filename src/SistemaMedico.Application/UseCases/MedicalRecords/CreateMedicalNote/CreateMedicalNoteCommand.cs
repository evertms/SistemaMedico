namespace SistemaMedico.Application.UseCases.MedicalRecords.CreateMedicalNote.CreateMedicalNoteCommand;

public class CreateMedicalNoteCommand
{
    public Guid DoctorId { get; init; }
    public Guid PatientId { get; init; }
    public string ChiefComplaint { get; init; }
    public string Observations { get; init; }
    public string TreatmentPlan { get; init; }
    public Guid DiagnosisId { get; init; }
    public Guid MedicalRecordId { get; init; }
    public Guid? AppointmentId { get; init; }
}
