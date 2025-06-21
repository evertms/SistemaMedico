using SistemaMedico.Application.Interfaces;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases;

public class SendReminderUseCase
{
    private readonly IMedicalAppointmentRepository _appointmentRepo;
    private readonly INotificationRepository _notificationRepo;

    public SendReminderUseCase(
        IMedicalAppointmentRepository appointmentRepo,
        INotificationRepository notificationRepo)
    {
        _appointmentRepo = appointmentRepo;
        _notificationRepo = notificationRepo;
    }

    public async Task ExecuteAsync()
    {
        var upcomingAppointments = await _appointmentRepo.GetNext24HoursAppointments();

        foreach (var appointment in upcomingAppointments)
        {
            var message = $"Tiene una cita programada con el Dr. {appointment.Doctor.LastName} el día {appointment.ScheduledDate.ToShortDateString()} a las {appointment.ScheduledDate.ToShortTimeString()}";
            
            var notification = new Notification(
                userId: appointment.Patient.UserId,
                title: "Recordatorio de Cita",
                message: message,
                scheduledAt: appointment.ScheduledDate.AddHours(-24)); // Programa 24h antes

            await _notificationRepo.AddAsync(notification);
            // Aquí también puedes disparar un servicio de email/push
        }
    }
}