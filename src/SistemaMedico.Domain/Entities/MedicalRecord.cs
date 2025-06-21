using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class MedicalRecord
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }

    public string? KnownAllergies { get; private set; }
    public string? ChronicConditions { get; private set; }
    public string? CurrentMedications { get; private set; }

    public ICollection<MedicalNote> Notes { get; private set; }
    public ICollection<Diagnosis> Diagnoses { get; private set; }

    public Patient Patient { get; private set; }

    protected MedicalRecord() 
    {
        Notes = new List<MedicalNote>();
        Diagnoses = new List<Diagnosis>();
    }

    public MedicalRecord(Guid patientId)
    {
        DomainValidation.EnsureNotEmptyGuid(patientId, nameof(patientId));

        Id = Guid.NewGuid();
        PatientId = patientId;
        Notes = new List<MedicalNote>();
        Diagnoses = new List<Diagnosis>();
    }

    public void AddNote(MedicalNote note)
    {
        DomainValidation.EnsureNotNull(note, nameof(note));

        if (note.PatientId != PatientId)
            throw new DomainException("La nota médica no pertenece al paciente de este historial.");

        Notes.Add(note);
    }

    public void AddDiagnosis(Diagnosis diagnosis)
    {
        DomainValidation.EnsureNotNull(diagnosis, nameof(diagnosis));

        if (diagnosis.PatientId != PatientId)
            throw new DomainException("El diagnóstico no pertenece al paciente de este historial.");

        if (diagnosis.IsConfirmed && diagnosis.MedicalNoteId is null)
            throw new DomainException("Un diagnóstico confirmado debe estar vinculado a una nota médica.");

        Diagnoses.Add(diagnosis);
    }

    public void UpdateMedicalData(
        string? knownAllergies = null,
        string? chronicConditions = null,
        string? currentMedications = null)
    {
        KnownAllergies = knownAllergies ?? KnownAllergies;
        ChronicConditions = chronicConditions ?? ChronicConditions;
        CurrentMedications = currentMedications ?? CurrentMedications;
    }

    public void EnsureBelongsToPatient(Guid expectedPatientId)
    {
        if (PatientId != expectedPatientId)
            throw new DomainException("Este historial no pertenece al paciente indicado.");
    }
}
