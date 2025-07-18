using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SistemaMedico.Application.Common.Interfaces.Services;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly string _jwtSecret;

    public JwtTokenService(string jwtSecret)
    {
        _jwtSecret = jwtSecret;
    }

    public string GenerateToken(User user, Guid? doctorId = null, Guid? patientId = null)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        if (patientId.HasValue)
            claims.Add(new Claim("patientId", patientId.Value.ToString()));

        if (doctorId.HasValue)
            claims.Add(new Claim("doctorId", doctorId.Value.ToString()));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "SistemaMedico",
            audience: "SistemaMedico",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
