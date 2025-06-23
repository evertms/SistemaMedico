using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.MedicalRecords.CreateDiagnosis;

public class CreateDiagnosisHandler : ICreateDiagnosisHandler
{
    private readonly IDiagnosisRepository _diagnosisRepository;
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public CreateDiagnosisHandler(
        IDiagnosisRepository diagnosisRepository,
        IMedicalRecordRepository medicalRecordRepository)
    {
        _diagnosisRepository = diagnosisRepository;
        _medicalRecordRepository = medicalRecordRepository;
    }

    public async Task<Guid> HandleAsync(CreateDiagnosisCommand command)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(command.MedicalRecordId);
        if (record is null || record.PatientId != command.PatientId)
            throw new DomainException("El historial no pertenece a este paciente.");

        var diagnosis = new Diagnosis(
            command.PatientId,
            command.DoctorId,
            command.Description,
            command.ICD10Code,
            command.IsConfirmed
        );

        if (command.IsConfirmed)
        {
            if (command.MedicalNoteId == null)
                throw new DomainException("Un diagnóstico confirmado debe estar asociado a una nota médica.");

            diagnosis.LinkToMedicalNote(command.MedicalNoteId.Value);
        }

        await _diagnosisRepository.AddAsync(diagnosis);
        return diagnosis.Id;
    }
}
