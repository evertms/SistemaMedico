using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.MedicalRecords.CreateMedicalNote;

public class CreateMedicalNoteHandler : ICreateMedicalNoteHandler
{
    private readonly IMedicalNoteRepository _noteRepository;
    private readonly IMedicalRecordRepository _recordRepository;
    private readonly IDiagnosisRepository _diagnosisRepository;

    public CreateMedicalNoteHandler(
        IMedicalNoteRepository noteRepository,
        IMedicalRecordRepository recordRepository,
        IDiagnosisRepository diagnosisRepository)
    {
        _noteRepository = noteRepository;
        _recordRepository = recordRepository;
        _diagnosisRepository = diagnosisRepository;
    }

    public async Task<Guid> HandleAsync(CreateMedicalNoteCommand.CreateMedicalNoteCommand command)
    {
        var record = await _recordRepository.GetByIdAsync(command.MedicalRecordId);
        if (record is null || record.PatientId != command.PatientId)
            throw new DomainException("El historial no pertenece a este paciente.");

        var diagnosis = await _diagnosisRepository.GetByIdAsync(command.DiagnosisId);
        if (diagnosis is null || diagnosis.PatientId != command.PatientId)
            throw new DomainException("El diagnóstico no corresponde a este paciente.");

        var note = new MedicalNote(
            command.PatientId,
            command.DoctorId,
            command.ChiefComplaint,
            command.Observations,
            diagnosis,
            command.TreatmentPlan,
            command.MedicalRecordId,
            command.AppointmentId
        );

        record.AddNote(note);

        await _noteRepository.AddAsync(note);
        return note.Id;
    }
}
