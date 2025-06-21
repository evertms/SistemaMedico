using SistemaMedico.Domain.Helpers;
using SistemaMedico.Domain.Exceptions;

namespace SistemaMedico.Domain.Entities;

public class Specialty
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    protected Specialty() { }

    public Specialty(string name, string description)
    {
        Validate(name);

        Id = Guid.NewGuid();
        Name = name;
        Description = description ?? string.Empty;
    }

    private void Validate(string name)
    {
        DomainValidation.EnsureNotNullOrEmpty(name, nameof(name));
    }

    public void UpdateName(string newName)
    {
        DomainValidation.EnsureNotNullOrEmpty(newName, nameof(newName));
        Name = newName;
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription ?? string.Empty;
    }
}