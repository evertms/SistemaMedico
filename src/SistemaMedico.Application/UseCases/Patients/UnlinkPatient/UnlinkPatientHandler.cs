using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Patients.UnlinkPatient;

public class UnlinkPatientHandler : IUnlinkPatientHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;

    public UnlinkPatientHandler(
        IUserRepository userRepository,
        IPatientRepository patientRepository)
    {
        _userRepository = userRepository;
        _patientRepository = patientRepository;
    }

    public async Task HandleAsync(UnlinkPatientCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
            throw new DomainException("Usuario no encontrado.");

        var patient = await _patientRepository.GetByIdAsync(command.PatientId);
        if (patient is null)
            throw new DomainException("Paciente no encontrado.");

        if (patient.UserId != command.UserId)
            throw new DomainException("Este paciente no está asociado a tu cuenta.");

        await _patientRepository.DeleteAsync(command.PatientId);
    }
}
