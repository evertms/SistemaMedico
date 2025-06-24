
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Notifications.SendAppointmentReminder;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Infrastructure.Services;

public class AppointmentReminderBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AppointmentReminderBackgroundService> _logger;

    public AppointmentReminderBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<AppointmentReminderBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var appointmentRepo = scope.ServiceProvider.GetRequiredService<IMedicalAppointmentRepository>();
            var handler = scope.ServiceProvider.GetRequiredService<ISendAppointmentReminderHandler>();

            var now = DateTime.Now;
            var from = now.AddHours(23.5);
            var to = now.AddHours(24.5);

            var appointments = await appointmentRepo.GetAppointmentsScheduledWithinAsync(from, to);

            foreach (var appt in appointments)
            {
                try
                {
                    var command = new SendAppointmentReminderCommand
                    {
                        AppointmentId = appt.Id
                    };

                    await handler.HandleAsync(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error enviando recordatorio para cita {appt.Id}");
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Ejecutar cada hora
        }
    }
}