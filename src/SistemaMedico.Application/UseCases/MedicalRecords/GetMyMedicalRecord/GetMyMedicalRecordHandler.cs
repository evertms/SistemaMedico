using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.MedicalRecords.GetMyMedicalRecord;

public class GetMyMedicalRecordHandler : IGetMyMedicalRecordHandler
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public GetMyMedicalRecordHandler(IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }

    public async Task<MedicalRecordDto> HandleAsync(GetMyMedicalRecordQuery query)
    {
        var record = await _medicalRecordRepository.GetMedicalRecordByPatientId(query.PatientId);
        if (record is null)
            throw new DomainException("No se encontró historial médico para este paciente.");

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
