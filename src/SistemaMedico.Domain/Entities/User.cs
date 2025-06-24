using SistemaMedico.Domain.Exceptions;
using SistemaMedico.Domain.Helpers;

namespace SistemaMedico.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PhoneNumber { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime RegistrationDate { get; private set; }

    public Doctor? Doctor { get; private set; }
    public Patient? Patient { get; private set; }

    public User() 
    {
    }

    public User(string email, string passwordHash, string phoneNumber, UserRole role)
    {
        Validate(email, passwordHash, phoneNumber);
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        Role = role;
        RegistrationDate = DateTime.UtcNow;
    }

    public void UpdateEmail(string newEmail)
    {
        DomainValidation.EnsureNotNullOrEmpty(newEmail, nameof(newEmail));
        DomainValidation.EnsureValidEmail(newEmail, nameof(newEmail));
        Email = newEmail;
    }

    public void UpdatePhoneNumber(string newPhoneNumber)
    {
        DomainValidation.EnsureNotNullOrEmpty(newPhoneNumber, nameof(newPhoneNumber));
        PhoneNumber = newPhoneNumber;
    }

    public void ChangeRole(UserRole newRole)
    {
        if (Role == newRole)
            return;

        if (Role == UserRole.Doctor && Doctor != null)
            throw new DomainException("No se puede cambiar el rol de médico si está asociado como Doctor.");

        if (Role == UserRole.Patient && Patient != null)
            throw new DomainException("No se puede cambiar el rol de paciente si hay pacientes vinculados.");

        Role = newRole;
    }

    private void Validate(string email, string passwordHash, string phoneNumber)
    {
        DomainValidation.EnsureNotNullOrEmpty(email, nameof(email));
        DomainValidation.EnsureNotNullOrEmpty(passwordHash, nameof(passwordHash));
        DomainValidation.EnsureNotNullOrEmpty(phoneNumber, nameof(phoneNumber));
        DomainValidation.EnsureValidEmail(email, nameof(email));
    }

    public void LinkPatient(Patient patient)
    {
        if (Patient is not null)
            throw new DomainException("Este usuario ya tiene un paciente asignado.");
        
        Patient = patient;
    }
}

public enum UserRole
{
    Patient,
    Doctor,
    Administrador
}
