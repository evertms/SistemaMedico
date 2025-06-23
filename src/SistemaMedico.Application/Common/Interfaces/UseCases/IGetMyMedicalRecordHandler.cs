using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.MedicalRecords.GetMyMedicalRecord;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetMyMedicalRecordHandler
{
    Task<MedicalRecordDto> HandleAsync(GetMyMedicalRecordQuery query);
}