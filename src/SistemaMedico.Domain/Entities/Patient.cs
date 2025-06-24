using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; }

    public User? User { get; private set; }
    private readonly List<MedicalAppointment> _appointments = new();
    public IReadOnlyCollection<MedicalAppointment> Appointments => _appointments.AsReadOnly();

    public MedicalRecord MedicalRecord { get; private set; }
    public ICollection<Diagnosis> Diagnoses { get; private set; }
    public ICollection<MedicalNote> MedicalNotes { get; private set; }

    protected Patient()
    {
        Diagnoses = new List<Diagnosis>();
        MedicalNotes = new List<MedicalNote>();
    }

    public Patient(Guid userId, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
    {
        Validate(firstName, lastName,dateOfBirth, phoneNumber);

        Id = Guid.NewGuid();
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;

        Diagnoses = new List<Diagnosis>();
        MedicalNotes = new List<MedicalNote>();
    }

    public void UpdatePatient(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
    {
        Validate(firstName, lastName, dateOfBirth, phoneNumber);

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
    }

    public int GetAge()
    {
        var today = DateTime.Today;
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth > today.AddYears(-age)) age--;
        return age;
    }

    private void Validate(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
    {
        DomainValidation.EnsureNotNullOrEmpty(firstName, nameof(firstName));
        DomainValidation.EnsureNotNullOrEmpty(lastName, nameof(lastName));
        DomainValidation.EnsureNotNullOrEmpty(phoneNumber, nameof(phoneNumber));
        DomainValidation.EnsureNotFutureDate(dateOfBirth, nameof(dateOfBirth));
    }

    public void AddAppointment(MedicalAppointment appointment)
    {
        if (appointment.PatientId != Id)
            throw new DomainException("La cita médica no pertenece a este paciente.");

        if (_appointments.Any(a =>
                a.ScheduledDate.Date == appointment.ScheduledDate.Date))
        {
            throw new DomainException("Ya existe una cita programada en esta fecha.");
        }

        if (_appointments.Any(a =>
                a.Status == AppointmentStatus.Pending ||
                a.Status == AppointmentStatus.Confirmed))
        {
            throw new DomainException("No se puede agregar una cita mientras exista una pendiente o confirmada.");
        }

        _appointments.Add(appointment);
    }
    
    public bool HasOverlappingAppointment(DateTime start, TimeSpan duration)
    {
        var end = start + duration;
        return Appointments.Any(a =>
        {
            var aStart = a.ScheduledDate;
            var aEnd = aStart + a.Duration;
            return aStart < end && start < aEnd;
        });
    }

}
