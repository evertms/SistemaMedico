namespace SistemaMedico.Application.Common.Interfaces.Services;

using SistemaMedico.Application.DTOs;

public interface IPdfGenerator
{
    byte[] Generate(MedicalRecordDto record);
}
