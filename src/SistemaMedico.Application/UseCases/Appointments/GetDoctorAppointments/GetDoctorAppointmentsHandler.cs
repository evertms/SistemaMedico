using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Application.DTOs;

namespace SistemaMedico.Application.UseCases.Appointments.GetDoctorAppointments;

public class GetDoctorAppointmentsHandler : IGetDoctorAppointmentsHandler
{
    private readonly IMedicalAppointmentRepository _appointmentRepository;

    public GetDoctorAppointmentsHandler(IMedicalAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<DoctorAppointmentDto>> HandleAsync(GetDoctorAppointmentsQuery query)
    {
        var appointments = await _appointmentRepository.GetByDoctorAsync(query.DoctorId);

        return appointments
            .Where(a => query.Date == null || a.ScheduledDate.Date == query.Date.Value.Date)
            .Select(a => new DoctorAppointmentDto
            {
                AppointmentId = a.Id,
                ScheduledDate = a.ScheduledDate,
                Duration = a.Duration,
                Status = a.Status.ToString(),
                PatientFullName = $"{a.Patient.FirstName} {a.Patient.LastName}"
            });
    }
}
