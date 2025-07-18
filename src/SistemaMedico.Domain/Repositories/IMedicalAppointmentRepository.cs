using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IMedicalAppointmentRepository : IRepository<MedicalAppointment>
{
    Task<bool> HasOverlappingAppointments(Guid doctorId, DateTime startDate, TimeSpan duration);
    Task<IEnumerable<MedicalAppointment>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<MedicalAppointment>> GetNext24HoursAppointments();
    Task<bool> IsScheduleAvailable(Guid scheduleId);
    Task<IEnumerable<MedicalAppointment>> GetByDoctorAsync(Guid doctorId);
    Task<List<MedicalAppointment>> GetAppointmentsScheduledWithinAsync(DateTime from, DateTime to);
    Task<List<MedicalAppointment>> GetPendingByPatientIdAsync(Guid patientId);
}