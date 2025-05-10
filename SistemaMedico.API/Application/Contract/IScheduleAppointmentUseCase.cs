using SistemaMedico.API.Models;

namespace SistemaMedico.API.Application.Contract;

public interface IScheduleAppointmentUseCase
{
    IEnumerable<Doctor> GetAvailableDoctors(string speciality);
    IEnumerable<DateTime> GetAvailableSlots();
    void ScheduleAppointment(AppointmentRequest request);
    void CancelAppointment(int appointmentId);
    void SendReminder(int idUser);
}