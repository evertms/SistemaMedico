namespace SistemaMedico.Application.UseCases.MedicalRecords.GetMyMedicalRecord;

public class GetMyMedicalRecordQuery
{
    public Guid PatientId { get; }

    public GetMyMedicalRecordQuery(Guid patientId)
    {
        PatientId = patientId;
    }
}

