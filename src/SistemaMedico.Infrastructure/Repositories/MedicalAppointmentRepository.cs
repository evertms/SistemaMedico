using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data.Context;

namespace SistemaMedico.Infrastructure.Repositories;

public class MedicalAppointmentRepository 
    : RepositoryBase<MedicalAppointment>, IMedicalAppointmentRepository
{
    public MedicalAppointmentRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<bool> HasOverlappingAppointments(Guid doctorId, DateTime startDate, TimeSpan duration)
    {
        var endDate = startDate.Add(duration);

        return await _dbSet.AnyAsync(a =>
            a.DoctorId == doctorId &&
            a.ScheduledDate < endDate &&
            a.ScheduledDate.Add(a.Duration) > startDate &&
            a.Status != AppointmentStatus.Cancelled);
    }

    public async Task<IEnumerable<MedicalAppointment>> GetByPatientIdAsync(Guid patientId)
    {
        return await _dbSet
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.ScheduledDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<MedicalAppointment>> GetNext24HoursAppointments()
    {
        var now = DateTime.UtcNow;
        var limit = now.AddHours(24);

        return await _dbSet
            .Where(a => a.ScheduledDate >= now &&
                        a.ScheduledDate <= limit &&
                        a.Status == AppointmentStatus.Confirmed)
            .ToListAsync();
    }

    public async Task<bool> IsScheduleAvailable(Guid scheduleId)
    {
        return !await _dbSet.AnyAsync(a => a.ScheduleId == scheduleId && a.Status != AppointmentStatus.Cancelled);
    }

    public async Task<IEnumerable<MedicalAppointment>> GetByDoctorAsync(Guid doctorId)
    {
        return await _dbSet
            .Where(a => a.DoctorId == doctorId)
            .OrderBy(a => a.ScheduledDate)
            .ToListAsync();
    }
    
    public async Task<List<MedicalAppointment>> GetAppointmentsScheduledWithinAsync(DateTime from, DateTime to)
    {
        return await _context.MedicalAppointments
            .Where(a => a.ScheduledDate >= from && a.ScheduledDate <= to && a.Status == AppointmentStatus.Pending)
            .Include(a => a.Patient)
            .ThenInclude(p => p.User)
            .ToListAsync();
    }

}