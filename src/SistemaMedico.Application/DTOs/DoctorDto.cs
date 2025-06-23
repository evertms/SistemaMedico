namespace SistemaMedico.Application.DTOs;

public class DoctorDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public Guid SpecialtyId { get; set; }
}
