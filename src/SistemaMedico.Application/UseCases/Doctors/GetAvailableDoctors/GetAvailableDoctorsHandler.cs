using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases.Doctors.GetAvailableDoctors;

public class GetAvailableDoctorsHandler : IGetAvailableDoctorsHandler
{
    private readonly IDoctorRepository _doctorRepository;

    public GetAvailableDoctorsHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<IEnumerable<DoctorDto>> HandleAsync(GetAvailableDoctorsQuery query)
    {
        var doctors = await _doctorRepository.GetAllAsync();

        var filtered = doctors
            .Where(d =>
                (!query.SpecialtyId.HasValue || d.SpecialtyId == query.SpecialtyId) &&
                (!query.Date.HasValue || d.Schedules.Any(s => s.StartDate.Date == query.Date.Value.Date && !s.IsBooked))
            );

        return filtered.Select(d => new DoctorDto
        {
            Id = d.Id,
            FullName = $"{d.FirstName} {d.LastName}",
            SpecialtyId = d.SpecialtyId
        });
    }
}
