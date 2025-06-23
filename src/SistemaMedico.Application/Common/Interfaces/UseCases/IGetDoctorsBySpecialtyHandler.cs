using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.Specialties.GetDoctorsBySpecialty;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetDoctorsBySpecialtyHandler
{
    Task<IEnumerable<DoctorDto>> HandleAsync(GetDoctorsBySpecialtyQuery query);
}