using SistemaMedico.Models;

namespace SistemaMedico.Services.Contract;

public interface IMedicalHistoryService
{
    void GetMedicalHistory();
    void UpdateMedicalHistory(Appointment appointment);
}