namespace SistemaMedico.API.Application.Contract;

public interface IPatientService
{
    void ScheduleAppointment();
    void CancelAppointment();
    void RescheduleAppointment();
}