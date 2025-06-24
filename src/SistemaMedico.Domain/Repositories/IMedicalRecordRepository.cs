using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<MedicalRecord?> GetMedicalRecordByPatientId(Guid patientId);
    Task<bool> IsNoteFromPatient(Guid medicalRecordId, Guid noteId, Guid patientId);
    Task<bool> IsDiagnosisLinkedToNote(Guid diagnosisId, Guid noteId);   
}