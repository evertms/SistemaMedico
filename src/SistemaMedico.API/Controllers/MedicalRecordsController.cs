using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.MedicalRecords.CreateMedicalNote;
using SistemaMedico.Application.UseCases.MedicalRecords.CreateDiagnosis;
using SistemaMedico.Application.UseCases.MedicalRecords.CreateMedicalNote.CreateMedicalNoteCommand;
using SistemaMedico.Application.UseCases.MedicalRecords.GetPatientMedicalRecord;
using SistemaMedico.Application.UseCases.MedicalRecords.GetMyMedicalRecord;
using SistemaMedico.Application.UseCases.MedicalRecords.DownloadMyMedicalRecord;

namespace SistemaMedico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordsController : ControllerBase
{
    private readonly ICreateMedicalNoteHandler _createMedicalNoteHandler;
    private readonly ICreateDiagnosisHandler _createDiagnosisHandler;
    private readonly IGetPatientMedicalRecordHandler _getPatientMedicalRecordHandler;
    private readonly IGetMyMedicalRecordHandler _getMyMedicalRecordHandler;
    private readonly IDownloadMyMedicalRecordHandler _downloadMyMedicalRecordHandler;

    public MedicalRecordsController(
        ICreateMedicalNoteHandler createMedicalNoteHandler,
        ICreateDiagnosisHandler createDiagnosisHandler,
        IGetPatientMedicalRecordHandler getPatientMedicalRecordHandler,
        IGetMyMedicalRecordHandler getMyMedicalRecordHandler,
        IDownloadMyMedicalRecordHandler downloadMyMedicalRecordHandler)
    {
        _createMedicalNoteHandler = createMedicalNoteHandler;
        _createDiagnosisHandler = createDiagnosisHandler;
        _getPatientMedicalRecordHandler = getPatientMedicalRecordHandler;
        _getMyMedicalRecordHandler = getMyMedicalRecordHandler;
        _downloadMyMedicalRecordHandler = downloadMyMedicalRecordHandler;
    }

    // POST: api/medicalrecords/{patientId}/note
    [HttpPost("{patientId}/note")]
    public async Task<IActionResult> CreateMedicalNote(Guid patientId, [FromBody] CreateMedicalNoteCommand command)
    {
        var newCommand = new CreateMedicalNoteCommand
        {
            PatientId = patientId,
            DoctorId = command.DoctorId,
            ChiefComplaint = command.ChiefComplaint,
            Observations = command.Observations,
            TreatmentPlan = command.TreatmentPlan,
            DiagnosisId = command.DiagnosisId,
            MedicalRecordId = command.MedicalRecordId,
            AppointmentId = command.AppointmentId
        };

        await _createMedicalNoteHandler.HandleAsync(newCommand);
        return NoContent();
    }

    // POST: api/medicalrecords/{patientId}/diagnosis
    [HttpPost("{patientId}/diagnosis")]
    public async Task<IActionResult> CreateDiagnosis(Guid patientId, [FromBody] CreateDiagnosisCommand command)
    {
        var newCommand = new CreateDiagnosisCommand
        {
            PatientId = patientId,
            DoctorId = command.DoctorId,
            ICD10Code = command.ICD10Code,
            Description = command.Description,
        };

        await _createDiagnosisHandler.HandleAsync(newCommand);
        return NoContent();
    }

    // GET: api/medicalrecords/{patientId}?doctorId={doctorId}
    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientMedicalRecord(Guid patientId, [FromQuery] Guid doctorId)
    {
        var query = new GetPatientMedicalRecordQuery(doctorId, patientId);
        var result = await _getPatientMedicalRecordHandler.HandleAsync(query);
        return Ok(result);
    }

    // GET: api/medicalrecords/me
    [HttpGet("me")]
    public async Task<IActionResult> GetMyMedicalRecord()
    {
        var patientId = GetUserId();
        var query = new GetMyMedicalRecordQuery(patientId);

        var result = await _getMyMedicalRecordHandler.HandleAsync(query);
        return Ok(result);
    }

    // GET: api/medicalrecords/me/download
    [HttpGet("me/download")]
    public async Task<IActionResult> DownloadMyMedicalRecord()
    {
        var patientId = GetUserId();
        var command = new DownloadMyMedicalRecordCommand
        {
            PatientId = patientId
        };

        var result = await _downloadMyMedicalRecordHandler.HandleAsync(command);

        return File(result.PdfContent, result.ContentType, result.FileName);
    }

    private Guid GetUserId()
    {
        var claim = User.Claims.FirstOrDefault(c => c.Type == "sub");
        return claim is not null ? Guid.Parse(claim.Value) : Guid.Empty;
    }
}
