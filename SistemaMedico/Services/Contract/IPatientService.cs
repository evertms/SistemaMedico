namespace SistemaMedico.Services.Contract;

public interface IPatientService
{
    void ScheduleAppointment();
    void CancelAppointment();
    void RescheduleAppointment();
}