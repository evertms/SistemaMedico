using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases.Appointments.GetMyPendingAppointments;

public class GetMyPendingAppointmentsHandler : IGetMyPendingAppointmentsHandler
{
    private readonly IMedicalAppointmentRepository _repository;

    public GetMyPendingAppointmentsHandler(IMedicalAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AppointmentDto>> HandleAsync(GetMyPendingAppointmentsQuery query)
    {
        var appointments = await _repository.GetPendingByPatientIdAsync(query.PatientId);

        return appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            DoctorName = $"{a.Doctor?.FirstName} {a.Doctor?.LastName}",
            ScheduledDate = a.ScheduledDate,
            Status = a.Status.ToString(),
            ReasonForVisit = a.ReasonForVisit
        });
    }
}
