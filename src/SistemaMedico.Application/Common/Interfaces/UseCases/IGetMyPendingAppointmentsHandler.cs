using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.Appointments.GetMyPendingAppointments;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetMyPendingAppointmentsHandler
{
    Task<IEnumerable<AppointmentDto>> HandleAsync(GetMyPendingAppointmentsQuery query);
}