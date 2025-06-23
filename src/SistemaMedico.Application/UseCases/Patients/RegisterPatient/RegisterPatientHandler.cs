using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Patients.RegisterPatient;

public class RegisterPatientHandler : IRegisterPatientHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;

    public RegisterPatientHandler(
        IUserRepository userRepository,
        IPatientRepository patientRepository)
    {
        _userRepository = userRepository;
        _patientRepository = patientRepository;
    }

    public async Task<Guid> HandleAsync(RegisterPatientCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
            throw new DomainException("Usuario no encontrado.");

        if (user.Role != UserRole.Patient)
            throw new DomainException("Solo usuarios con rol 'Patient' pueden registrar pacientes.");

        var patient = new Patient(
            userId: command.UserId,
            firstName: command.FirstName,
            lastName: command.LastName,
            dateOfBirth: command.DateOfBirth,
            phoneNumber: command.PhoneNumber
        );

        await _patientRepository.AddAsync(patient);

        user.LinkPatient(patient); // lógica de dominio

        return patient.PatientId;
    }
}
