namespace SistemaMedico.Application.UseCases.Appointments.GetMyPendingAppointments;

public class GetMyPendingAppointmentsQuery
{
    public Guid PatientId { get; }

    public GetMyPendingAppointmentsQuery(Guid patientId)
    {
        PatientId = patientId;
    }
}
