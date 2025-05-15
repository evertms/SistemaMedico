using SistemaMedico.API.Models;

namespace SistemaMedico.API.Application.Contract;

public interface IMedicalHistoryService
{
    void GetMedicalHistory();
    void UpdateMedicalHistory(Appointment appointment);
}