namespace SistemaMedico.API.Application.Contract;

public interface IDoctorService
{
    void RecordDiagnosis();
    void RequestAnalysis();
    void Prescribe();
}