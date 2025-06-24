using SistemaMedico.Application.Common.Interfaces.UseCases;

namespace SistemaMedico.Application.UseCases.Specialties.GetAllSpecialties;

using SistemaMedico.Domain.Repositories;
using SistemaMedico.Application.DTOs;

public class GetAllSpecialtiesHandler : IGetAllSpecialtiesHandler
{
    private readonly ISpecialtyRepository _repository;

    public GetAllSpecialtiesHandler(ISpecialtyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SpecialtyDto>> HandleAsync()
    {
        var all = await _repository.GetAllAsync();

        return all.Select(s => new SpecialtyDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description
        });
    }
}
