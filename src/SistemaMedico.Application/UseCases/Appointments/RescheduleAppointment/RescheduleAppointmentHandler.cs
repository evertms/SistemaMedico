using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Appointments.RescheduleAppointment;

public class RescheduleAppointmentHandler : IRescheduleAppointmentHandler
{
    private readonly IMedicalAppointmentRepository _appointmentRepository;
    private readonly IAvailableDoctorScheduleRepository _scheduleRepository;

    public RescheduleAppointmentHandler(
        IMedicalAppointmentRepository appointmentRepository,
        IAvailableDoctorScheduleRepository scheduleRepository)
    {
        _appointmentRepository = appointmentRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task HandleAsync(RescheduleAppointmentCommand command)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(command.AppointmentId);
        if (appointment is null)
            throw new DomainException("La cita no existe.");

        if (appointment.PatientId != command.PatientId)
            throw new DomainException("No tienes permiso para reprogramar esta cita.");

        var oldSchedule = await _scheduleRepository.GetByIdAsync(appointment.ScheduleId);
        oldSchedule?.Release();
        
        var newSchedule = await _scheduleRepository.GetByIdAsync(command.NewScheduleId);
        if (newSchedule is null || newSchedule.IsBooked)
            throw new DomainException("El nuevo horario no está disponible.");

        appointment.Reschedule(command.NewDate, command.NewScheduleId);
        newSchedule.MarkAsBooked();

        await _appointmentRepository.UpdateAsync(appointment);
        await _scheduleRepository.UpdateAsync(newSchedule);

        if (oldSchedule is not null)
            await _scheduleRepository.UpdateAsync(oldSchedule);
    }
}
