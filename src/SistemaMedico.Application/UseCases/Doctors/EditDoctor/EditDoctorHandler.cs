using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases.Doctors.EditDoctor;

public class EditDoctorHandler : IEditDoctorHandler
{
    private readonly IDoctorRepository _doctorRepository;

    public EditDoctorHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task HandleAsync(EditDoctorCommand command)
    {
        var doctor = await _doctorRepository.GetByIdAsync(command.DoctorId);
        if (doctor is null)
            throw new DomainException("Médico no encontrado.");

        doctor.UpdateLicense(command.NewLicenseNumber);
        doctor.UpdateSpecialty(command.NewSpecialtyId);

        await _doctorRepository.UpdateAsync(doctor);
    }
}
