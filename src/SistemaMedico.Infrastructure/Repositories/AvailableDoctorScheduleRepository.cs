using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class AvailableDoctorScheduleRepository 
    : RepositoryBase<AvailableDoctorSchedule>, IAvailableDoctorScheduleRepository
{
    public AvailableDoctorScheduleRepository(ApplicationDbContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<AvailableDoctorSchedule>> GetAvailableByDoctorIdAsync(Guid doctorId)
    {
        return await _dbSet
            .Where(s => s.DoctorId == doctorId && !s.IsBooked && s.StartDate >= DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<bool> ExceedsMaxAppointmentsPerDay(Guid doctorId, int maxAppointments)
    {
        var today = DateTime.UtcNow.Date;

        var count = await _dbSet
            .CountAsync(s =>
                s.DoctorId == doctorId &&
                s.StartDate.Date == today &&
                s.IsBooked);

        return count >= maxAppointments;
    }

    public async Task<bool> OverlapsWith(Guid doctorId, DateTime newDate, TimeSpan duration)
    {
        var newEnd = newDate.Add(duration);

        return await _dbSet
            .AnyAsync(s =>
                s.DoctorId == doctorId &&
                s.StartDate < newEnd &&
                s.StartDate.Add(s.Duration) > newDate);
    }
}
