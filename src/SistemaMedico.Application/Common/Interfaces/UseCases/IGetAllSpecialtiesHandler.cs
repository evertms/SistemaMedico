using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.Specialties.GetAllSpecialties;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetAllSpecialtiesHandler
{
    Task<IEnumerable<SpecialtyDto>> HandleAsync();
}