using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases.Doctors.GetAvailableSchedules;

public class GetAvailableSchedulesHandler : IGetAvailableSchedulesHandler
{
    private readonly IAvailableDoctorScheduleRepository _scheduleRepository;

    public GetAvailableSchedulesHandler(IAvailableDoctorScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<IEnumerable<ScheduleDto>> HandleAsync(GetAvailableSchedulesQuery query)
    {
        var allSchedules = await _scheduleRepository.GetAvailableByDoctorIdAsync(query.DoctorId);

        var filtered = allSchedules
            .Where(s => !s.IsBooked &&
                        (!query.Date.HasValue || s.StartDate.Date == query.Date.Value.Date));

        return filtered.Select(s => new ScheduleDto
        {
            ScheduleId = s.Id,
            DoctorId = s.DoctorId,
            StartDate = s.StartDate,
            Duration = s.Duration
        });
    }
}
