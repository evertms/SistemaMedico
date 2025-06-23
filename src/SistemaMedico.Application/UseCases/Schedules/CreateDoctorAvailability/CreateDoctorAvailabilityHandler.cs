using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Schedules.CreateDoctorAvailability;

public class CreateDoctorAvailabilityHandler : ICreateDoctorAvailabilityHandler
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAvailableDoctorScheduleRepository _scheduleRepository;

    public CreateDoctorAvailabilityHandler(
        IDoctorRepository doctorRepository,
        IAvailableDoctorScheduleRepository scheduleRepository)
    {
        _doctorRepository = doctorRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Guid> HandleAsync(CreateDoctorAvailabilityCommand command)
    {
        var doctor = await _doctorRepository.GetByIdAsync(command.DoctorId);
        if (doctor is null)
            throw new DomainException("El médico no existe.");

        var existingSchedules = await _scheduleRepository.GetAvailableByDoctorIdAsync(command.DoctorId);
        var newSchedule = new AvailableDoctorSchedule(command.DoctorId, command.StartDate, command.Duration);

        // Verificar solapamiento con otros horarios
        if (existingSchedules.Any(s => newSchedule.OverlapsWith(s)))
            throw new DomainException("El nuevo horario se solapa con otro ya existente.");

        await _scheduleRepository.AddAsync(newSchedule);
        return newSchedule.Id;
    }
}
