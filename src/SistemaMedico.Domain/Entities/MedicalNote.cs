using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class MedicalNote
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid? AppointmentId { get; private set; }
    public Guid MedicalRecordId { get; private set; }
    public Guid DiagnosisId { get; private set; }

    public string ChiefComplaint { get; private set; }
    public string Observations { get; private set; }
    public Diagnosis Diagnosis { get; private set; }
    public string TreatmentPlan { get; private set; }
    public DateTime RecordDate { get; private set; }

    // Relaciones
    public Patient Patient { get; private set; }
    public Doctor Doctor { get; private set; }
    public MedicalAppointment? Appointment { get; private set; }
    public MedicalRecord? MedicalRecord { get; private set; }

    public MedicalNote() { }

    public MedicalNote(
        Guid patientId,
        Guid doctorId,
        string chiefComplaint,
        string observations,
        Diagnosis diagnosis,
        string treatmentPlan,
        Guid medicalRecordId,
        Guid? appointmentId = null)
    {
        Validate(patientId, doctorId, chiefComplaint, diagnosis, treatmentPlan);

        Id = Guid.NewGuid();
        PatientId = patientId;
        DoctorId = doctorId;
        ChiefComplaint = chiefComplaint;
        Observations = observations;
        Diagnosis = diagnosis;
        DiagnosisId = diagnosis.Id;
        TreatmentPlan = treatmentPlan;
        AppointmentId = appointmentId;
        MedicalRecordId = medicalRecordId;
        RecordDate = DateTime.UtcNow;
    }

    private void Validate(Guid patientId, Guid doctorId, string complaint, Diagnosis diagnosis, string treatment)
    {
        DomainValidation.EnsureNotEmptyGuid(patientId, nameof(patientId));
        DomainValidation.EnsureNotEmptyGuid(doctorId, nameof(doctorId));
        DomainValidation.EnsureNotNullOrEmpty(complaint, nameof(complaint));
        DomainValidation.EnsureNotNullOrEmpty(treatment, nameof(treatment));

        if (diagnosis == null)
            throw new DomainException("Debe proporcionarse un diagnóstico válido.");
    }

    public void UpdateTreatmentPlan(string newTreatmentPlan)
    {
        DomainValidation.EnsureNotNullOrEmpty(newTreatmentPlan, nameof(newTreatmentPlan));
        TreatmentPlan = newTreatmentPlan;
    }

    public void UpdateObservations(string newObservations)
    {
        DomainValidation.EnsureNotNullOrEmpty(newObservations, nameof(newObservations));
        Observations = newObservations;
    }

    public void EnsureBelongsToPatient(Guid expectedPatientId)
    {
        if (PatientId != expectedPatientId)
            throw new DomainException("La nota médica no pertenece al paciente especificado.");
    }
}
