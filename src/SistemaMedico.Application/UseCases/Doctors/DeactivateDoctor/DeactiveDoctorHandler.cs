using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases.Doctors.DeactivateDoctor;

public class DeactivateDoctorHandler : IDeactivateDoctorHandler
{
    private readonly IDoctorRepository _doctorRepository;

    public DeactivateDoctorHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task HandleAsync(DeactivateDoctorCommand command)
    {
        var doctor = await _doctorRepository.GetByIdAsync(command.DoctorId);
        if (doctor is null)
            throw new DomainException("Médico no encontrado.");

        doctor.Deactivate();
        await _doctorRepository.UpdateAsync(doctor);
    }
}
