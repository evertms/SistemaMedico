using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.Appointments.GetDoctorAppointments;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetDoctorAppointmentsHandler
{
    Task<IEnumerable<DoctorAppointmentDto>> HandleAsync(GetDoctorAppointmentsQuery query);
}