using SistemaMedico.Application.UseCases.MedicalRecords.CreateMedicalNote.CreateMedicalNoteCommand;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface ICreateMedicalNoteHandler
{
    Task<Guid> HandleAsync(CreateMedicalNoteCommand command);
}