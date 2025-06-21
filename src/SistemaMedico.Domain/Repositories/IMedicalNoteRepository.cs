using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IMedicalNoteRepository : IRepository<MedicalNote>
{
    Task<bool> BelongsToPatient(Guid noteId, Guid patientId);
    Task<IEnumerable<MedicalNote>> GetNotesByAppointment(Guid appointmentId);   
}