using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Application.Common.Interfaces.Services;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Application.UseCases.Notifications.SendAppointmentReminder;

public class SendAppointmentReminderHandler : ISendAppointmentReminderHandler
{
    private readonly IMedicalAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly INotificationRepository _notificationRepository;
    public SendAppointmentReminderHandler(
        IMedicalAppointmentRepository appointmentRepository,
        IUserRepository userRepository,
        IEmailService emailService,
        INotificationRepository notificationRepository)
    {
        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
        _emailService = emailService;
        _notificationRepository = notificationRepository;
    }

    public async Task HandleAsync(SendAppointmentReminderCommand command)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(command.AppointmentId);
        if (appointment == null)
            throw new DomainException("Cita no encontrada.");

        var patientUser = await _userRepository.GetByIdAsync(appointment.Patient!.UserId);
        if (patientUser == null)
            throw new DomainException("Usuario asociado al paciente no encontrado.");

        var subject = "Recordatorio de consulta médica";
        var body = $"Hola, te recordamos tu cita programada el {appointment.ScheduledDate:f}.";
        
        var notification = new Notification(patientUser.Id, subject, body, DateTime.Now, appointment.Id);
        
        await _notificationRepository.AddAsync(notification);
        await _emailService.SendAsync(patientUser.Email, subject, body);
    }
}
