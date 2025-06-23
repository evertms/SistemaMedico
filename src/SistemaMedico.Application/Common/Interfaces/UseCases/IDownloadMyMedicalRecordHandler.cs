using SistemaMedico.Application.UseCases.MedicalRecords.DownloadMyMedicalRecord;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IDownloadMyMedicalRecordHandler
{
    Task<DownloadMyMedicalRecordResponse> HandleAsync(DownloadMyMedicalRecordCommand command);
}