namespace SistemaMedico.Application.UseCases.Auth.Login;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? PatientId { get; set; }
}
