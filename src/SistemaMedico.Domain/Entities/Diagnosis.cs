using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class Diagnosis
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }

    public string Description { get; private set; }
    public string? ICD10Code { get; private set; }

    public bool IsConfirmed { get; private set; }
    public DateTime DateRecorded { get; private set; }

    public Guid? MedicalNoteId { get; private set; }
    public MedicalNote? MedicalNote { get; private set; }

    public Patient Patient { get; private set; }
    public Doctor Doctor { get; private set; }

    protected Diagnosis() { }

    public Diagnosis(
        Guid patientId,
        Guid doctorId,
        string description,
        string? icd10Code = null,
        bool isConfirmed = false,
        Guid? medicalNoteId = null)
    {
        Validate(patientId, doctorId, description, isConfirmed, medicalNoteId);

        Id = Guid.NewGuid();
        PatientId = patientId;
        DoctorId = doctorId;
        Description = description;
        ICD10Code = icd10Code;
        IsConfirmed = isConfirmed;
        MedicalNoteId = medicalNoteId;
        DateRecorded = DateTime.UtcNow;
    }

    private void Validate(Guid patientId, Guid doctorId, string description, bool isConfirmed, Guid? medicalNoteId)
    {
        DomainValidation.EnsureNotEmptyGuid(patientId, nameof(patientId));
        DomainValidation.EnsureNotEmptyGuid(doctorId, nameof(doctorId));
        DomainValidation.EnsureNotNullOrEmpty(description, nameof(description));

        if (isConfirmed && medicalNoteId is null)
            throw new DomainException("Un diagnóstico confirmado debe estar asociado a una nota médica.");
    }

    public void Confirm()
    {
        if (IsConfirmed)
            return;

        if (MedicalNoteId is null)
            throw new DomainException("No se puede confirmar un diagnóstico sin asociarlo a una nota médica.");

        IsConfirmed = true;
    }

    public void LinkToMedicalNote(Guid medicalNoteId)
    {
        DomainValidation.EnsureNotEmptyGuid(medicalNoteId, nameof(medicalNoteId));
        MedicalNoteId = medicalNoteId;
    }

    public void UpdateDescription(string newDescription)
    {
        DomainValidation.EnsureNotNullOrEmpty(newDescription, nameof(newDescription));
        Description = newDescription;
    }

    public void UpdateICD10(string newCode)
    {
        ICD10Code = newCode;
    }
}
