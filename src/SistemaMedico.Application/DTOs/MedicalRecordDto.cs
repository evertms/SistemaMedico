namespace SistemaMedico.Application.DTOs;

public class MedicalRecordDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string? Allergies { get; set; }
    public string? Conditions { get; set; }
    public string? Medications { get; set; }
    public int NoteCount { get; set; }
    public int DiagnosisCount { get; set; }
}
