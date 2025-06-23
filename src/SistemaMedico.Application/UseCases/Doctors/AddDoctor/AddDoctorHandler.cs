using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Domain.Repositories;

namespace SistemaMedico.Application.UseCases.Doctors.AddDoctor;

public class AddDoctorHandler : IAddDoctorHandler
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUserRepository _userRepository;

    public AddDoctorHandler(IDoctorRepository doctorRepository, IUserRepository userRepository)
    {
        _doctorRepository = doctorRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> HandleAsync(AddDoctorCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
            throw new DomainException("Usuario no encontrado.");

        if (user.Role != UserRole.Doctor)
            throw new DomainException("Este usuario no tiene rol de médico.");

        var doctor = new Doctor(
            command.UserId,
            command.SpecialtyId,
            command.FirstName,
            command.LastName,
            command.LicenseNumber
        );

        await _doctorRepository.AddAsync(doctor);
        return doctor.Id;
    }
}
