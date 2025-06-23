namespace SistemaMedico.Application.UseCases.MedicalRecords.DownloadMyMedicalRecord;

public class DownloadMyMedicalRecordResponse
{
    public byte[] PdfContent { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; } = "application/pdf";
}
