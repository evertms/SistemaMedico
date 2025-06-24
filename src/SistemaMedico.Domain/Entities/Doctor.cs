using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class Doctor
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string LicenseNumber { get; private set; }

    public Guid SpecialtyId { get; private set; }
    public Specialty Specialty { get; private set; }

    public DateTime HireDate { get; private set; }
    public DateTime? DeactivationDate { get; private set; }

    public ICollection<MedicalAppointment> Appointments { get; private set; }
    public ICollection<Diagnosis> Diagnosis { get; private set; }
    public ICollection<MedicalNote> MedicalNotes { get; private set; }
    public ICollection<AvailableDoctorSchedule> Schedules { get; private set; }

    public Doctor()
    {
        Appointments = new List<MedicalAppointment>();
        Diagnosis = new List<Diagnosis>();
        MedicalNotes = new List<MedicalNote>();
        Schedules = new List<AvailableDoctorSchedule>();
    }

    public Doctor(
        Guid userId,
        Guid specialtyId,
        string firstName,
        string lastName,
        string licenseNumber
    )
    {
        Validate(firstName, lastName, licenseNumber, specialtyId);

        Id = Guid.NewGuid();
        UserId = userId;
        SpecialtyId = specialtyId;
        FirstName = firstName;
        LastName = lastName;
        LicenseNumber = licenseNumber;
        HireDate = DateTime.UtcNow;

        Appointments = new List<MedicalAppointment>();
        Diagnosis = new List<Diagnosis>();
        MedicalNotes = new List<MedicalNote>();
        Schedules = new List<AvailableDoctorSchedule>();
    }

    private void Validate(string firstName, string lastName, string licenseNumber, Guid specialtyId)
    {
        DomainValidation.EnsureNotNullOrEmpty(firstName, nameof(firstName));
        DomainValidation.EnsureNotNullOrEmpty(lastName, nameof(lastName));
        DomainValidation.EnsureNotNullOrEmpty(licenseNumber, nameof(licenseNumber));

        if (licenseNumber.Length < 6)
            throw new DomainException("El número de licencia médica debe tener al menos 6 caracteres.");

        if (specialtyId == Guid.Empty)
            throw new DomainException("El médico debe tener una especialidad asignada.");
    }

    public void UpdateLicense(string newLicense)
    {
        DomainValidation.EnsureNotNullOrEmpty(newLicense, nameof(newLicense));

        if (newLicense.Length < 6)
            throw new DomainException("El número de licencia médica debe tener al menos 6 caracteres.");

        LicenseNumber = newLicense;
    }

    public void UpdateSpecialty(Guid newSpecialtyId)
    {
        if (newSpecialtyId == Guid.Empty)
            throw new DomainException("La especialidad no puede estar vacía.");

        SpecialtyId = newSpecialtyId;
    }

    public IEnumerable<MedicalAppointment> GetPendingAppointments()
    {
        return Appointments.Where(a => a.Status == AppointmentStatus.Pending);
    }
    
    public void Deactivate()
    {
        if (DeactivationDate is not null)
            throw new DomainException("El médico ya fue dado de baja.");
        DeactivationDate = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        DeactivationDate = null;
    }
}
