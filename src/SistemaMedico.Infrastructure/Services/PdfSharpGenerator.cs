namespace SistemaMedico.Infrastructure.Services;

using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using SistemaMedico.Application.Common.Interfaces.Services;
using SistemaMedico.Application.DTOs;

public class PdfSharpGenerator : IPdfGenerator
{
    public byte[] Generate(MedicalRecordDto record)
    {
        using var doc = new PdfDocument();
        var page = doc.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Verdana", 12);

        gfx.DrawString("Historial Médico", font, XBrushes.Black, 20, 30);
        gfx.DrawString($"Paciente ID: {record.PatientId}", font, XBrushes.Black, 20, 60);
        gfx.DrawString($"Alergias: {record.Allergies}", font, XBrushes.Black, 20, 90);
        gfx.DrawString($"Condiciones crónicas: {record.Conditions}", font, XBrushes.Black, 20, 120);
        gfx.DrawString($"Medicamentos actuales: {record.Medications}", font, XBrushes.Black, 20, 150);
        gfx.DrawString($"Notas médicas: {record.NoteCount}", font, XBrushes.Black, 20, 180);
        gfx.DrawString($"Diagnósticos: {record.DiagnosisCount}", font, XBrushes.Black, 20, 210);

        using var ms = new MemoryStream();
        doc.Save(ms);
        return ms.ToArray();
    }
}
