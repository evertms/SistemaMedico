using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.UseCases.Doctors.GetAvailableDoctors;

namespace SistemaMedico.Application.Common.Interfaces.UseCases;

public interface IGetAvailableDoctorsHandler
{
    Task<IEnumerable<DoctorDto>> HandleAsync(GetAvailableDoctorsQuery query);
}