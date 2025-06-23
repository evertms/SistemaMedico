using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.MedicalRecords.GetPatientMedicalRecord;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetPatientMedicalRecordHandler
{
    Task<MedicalRecordDto> HandleAsync(GetPatientMedicalRecordQuery query);
}