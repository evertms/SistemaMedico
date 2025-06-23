using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.Doctors.GetAvailableSchedules;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetAvailableSchedulesHandler
{
    Task<IEnumerable<ScheduleDto>> HandleAsync(GetAvailableSchedulesQuery query);
}