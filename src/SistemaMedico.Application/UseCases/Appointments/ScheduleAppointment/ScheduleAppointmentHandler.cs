using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Application.UseCases.Appointments.ScheduleAppointment;

public class ScheduleAppointmentHandler : IScheduleAppointmentHandler
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAvailableDoctorScheduleRepository _scheduleRepository;
    private readonly IMedicalAppointmentRepository _appointmentRepository;

    public ScheduleAppointmentHandler(
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IAvailableDoctorScheduleRepository scheduleRepository,
        IMedicalAppointmentRepository appointmentRepository)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _scheduleRepository = scheduleRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Guid> HandleAsync(ScheduleAppointmentCommand command)
    {
        // 1. Verificar existencia del paciente
        var patient = await _patientRepository.GetByIdAsync(command.PatientId);
        if (patient is null)
            throw new DomainException("Paciente no encontrado.");

        // 2. Verificar existencia del médico
        var doctor = await _doctorRepository.GetByIdAsync(command.DoctorId);
        if (doctor is null)
            throw new DomainException("Médico no encontrado.");

        // 3. Verificar disponibilidad del horario
        var schedule = await _scheduleRepository.GetByIdAsync(command.ScheduleId);
        if (schedule is null || schedule.IsBooked)
            throw new DomainException("El horario no está disponible.");

        // 4. Verificar solapamientos con otras citas del paciente
        if (patient.HasOverlappingAppointment(schedule.StartDate, command.Duration))
            throw new DomainException("El paciente ya tiene una cita en ese horario.");

        // 5. Crear la cita
        var appointment = new MedicalAppointment(
            patientId: command.PatientId,
            doctorId: command.DoctorId,
            scheduleId: command.ScheduleId,
            scheduledDate: schedule.StartDate,
            duration: command.Duration,
            reasonForVisit: command.ReasonForVisit
        );

        // 6. Asociar cita al paciente y marcar horario como reservado
        patient.AddAppointment(appointment);
        schedule.MarkAsBooked();

        // 7. Persistir
        await _appointmentRepository.AddAsync(appointment);
        await _scheduleRepository.UpdateAsync(schedule);

        return appointment.Id;
    }
}
