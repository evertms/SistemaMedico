namespace SistemaMedico.Application.UseCases.MedicalRecords.GetPatientMedicalRecord;


public class GetPatientMedicalRecordQuery
{
    public Guid DoctorId { get; }
    public Guid PatientId { get; }

    public GetPatientMedicalRecordQuery(Guid doctorId, Guid patientId)
    {
        DoctorId = doctorId;
        PatientId = patientId;
    }
}