using System.Security.Claims;

namespace SistemaMedico.Application.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetPatientId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("patientId")?.Value;
        if (string.IsNullOrWhiteSpace(claim))
            throw new UnauthorizedAccessException("No se encontró el ID del paciente en el token.");

        return Guid.Parse(claim);
    }

    public static Guid GetDoctorId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("doctorId")?.Value;
        if (string.IsNullOrWhiteSpace(claim))
            throw new UnauthorizedAccessException("No se encontró el ID del médico en el token.");

        return Guid.Parse(claim);
    }
}