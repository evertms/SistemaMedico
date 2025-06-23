using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases.Specialties.GetDoctorsBySpecialty;

public class GetDoctorsBySpecialtyHandler : IGetDoctorsBySpecialtyHandler
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsBySpecialtyHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<IEnumerable<DoctorDto>> HandleAsync(GetDoctorsBySpecialtyQuery query)
    {
        var allDoctors = await _doctorRepository.GetAllAsync();

        var filtered = allDoctors
            .Where(d => d.SpecialtyId == query.SpecialtyId &&
                        (query.Date == null ||
                         d.Schedules.Any(s =>
                             s.StartDate.Date == query.Date.Value.Date &&
                             !s.IsBooked)
                         && d.DeactivationDate == null));

        return filtered.Select(d => new DoctorDto
        {
            Id = d.Id,
            FullName = $"{d.FirstName} {d.LastName}",
            SpecialtyId = d.SpecialtyId
        });
    }
}
