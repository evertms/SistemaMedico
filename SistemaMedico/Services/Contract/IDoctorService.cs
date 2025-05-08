namespace SistemaMedico.Services.Contract;

public interface IDoctorService
{
    void RecordDiagnosis();
    void RequestAnalysis();
    void Prescribe();
}