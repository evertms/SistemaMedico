using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    Task<bool> HasActiveAppointment(Guid patientId);
    Task<bool> ExistsByIdAsync(Guid patientId);
    Task<bool> ExistsWithEmailAsync(string email);
}