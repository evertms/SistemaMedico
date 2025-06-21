using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Domain.Repositories;

public interface IAvailableDoctorScheduleRepository : IRepository<AvailableDoctorSchedule>
{
    Task<IEnumerable<AvailableDoctorSchedule>> GetAvailableByDoctorIdAsync(Guid doctorId);
    Task<bool> ExceedsMaxAppointmentsPerDay(Guid doctorId, int maxAppointments);
    Task<bool> OverlapsWith(Guid doctorId, DateTime newDate, TimeSpan duration);   
}