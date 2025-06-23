using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Application.Common.Interfaces.Services;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;

namespace SistemaMedico.Application.UseCases.MedicalRecords.DownloadMyMedicalRecord;

public class DownloadMyMedicalRecordHandler : IDownloadMyMedicalRecordHandler
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IPdfGenerator _pdfGenerator;

    public DownloadMyMedicalRecordHandler(
        IMedicalRecordRepository medicalRecordRepository,
        IPdfGenerator pdfGenerator)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _pdfGenerator = pdfGenerator;
    }

    public async Task<DownloadMyMedicalRecordResponse> HandleAsync(DownloadMyMedicalRecordCommand command)
    {
        var record = await _medicalRecordRepository.GetMedicalRecordByPatientId(command.PatientId);
        if (record == null)
            throw new DomainException("No se encontró el historial médico del paciente.");

        var dto = new MedicalRecordDto
        {
            Id = record.Id,
            PatientId = record.PatientId,
            Allergies = record.KnownAllergies,
            Conditions = record.ChronicConditions,
            Medications = record.CurrentMedications,
            NoteCount = record.Notes.Count,
            DiagnosisCount = record.Diagnoses.Count
        };

        var pdf = _pdfGenerator.Generate(dto);

        return new DownloadMyMedicalRecordResponse
        {
            PdfContent = pdf,
            FileName = $"historial_{command.PatientId}.pdf"
        };
    }
}
