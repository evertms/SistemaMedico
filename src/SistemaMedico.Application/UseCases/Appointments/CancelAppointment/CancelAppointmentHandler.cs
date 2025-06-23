using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Appointments.CancelAppointment;

public class CancelAppointmentHandler : ICancelAppointmentHandler
{
    private readonly IMedicalAppointmentRepository _appointmentRepository;

    public CancelAppointmentHandler(IMedicalAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task HandleAsync(CancelAppointmentCommand command)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(command.AppointmentId);
        if (appointment is null)
            throw new DomainException("La cita no existe.");

        if (appointment.PatientId != command.PatientId)
            throw new DomainException("No tienes permiso para cancelar esta cita.");

        appointment.Cancel();

        await _appointmentRepository.UpdateAsync(appointment);
    }
}
