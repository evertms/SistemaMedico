using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.MedicalRecords.GetPatientMedicalRecord;

public class GetPatientMedicalRecordHandler : IGetPatientMedicalRecordHandler
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IDoctorRepository _doctorRepository;

    public GetPatientMedicalRecordHandler(
        IMedicalRecordRepository medicalRecordRepository,
        IDoctorRepository doctorRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<MedicalRecordDto> HandleAsync(GetPatientMedicalRecordQuery query)
    {
        var doctor = await _doctorRepository.GetByIdAsync(query.DoctorId);
        if (doctor == null)
            throw new DomainException("El médico no existe.");

        var record = await _medicalRecordRepository.GetMedicalRecordByPatientId(query.PatientId);
        if (record is null)
            throw new DomainException("El paciente no tiene historial médico registrado.");

        // (Opcional) Validar relación doctor-paciente a futuro.

        return new MedicalRecordDto
        {
            Id = record.Id,
            PatientId = record.PatientId,
            Allergies = record.KnownAllergies,
            Conditions = record.ChronicConditions,
            Medications = record.CurrentMedications,
            NoteCount = record.Notes.Count,
            DiagnosisCount = record.Diagnoses.Count
        };
    }
}
