using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Appointments.CancelAppointment;

public class CancelAppointmentHandler : ICancelAppointmentHandler
{
    private readonly IMedicalAppointmentRepository _appointmentRepository;
    private readonly IAvailableDoctorScheduleRepository _scheduleRepository;
    
    public CancelAppointmentHandler(
        IMedicalAppointmentRepository appointmentRepository,
        IAvailableDoctorScheduleRepository scheduleRepository)
    {
        _appointmentRepository = appointmentRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task HandleAsync(CancelAppointmentCommand command)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(command.AppointmentId);
        if (appointment is null)
            throw new DomainException("La cita no existe.");
        
        if (appointment.PatientId != command.PatientId)
            throw new DomainException("No tienes permiso para cancelar esta cita.");
        
        var schedule = await _scheduleRepository.GetByIdAsync(appointment.ScheduleId);
        schedule?.Release();
        
        appointment.Cancel();

        await _appointmentRepository.UpdateAsync(appointment);
        if (schedule is not null)
            await _scheduleRepository.UpdateAsync(schedule);
    }
}
