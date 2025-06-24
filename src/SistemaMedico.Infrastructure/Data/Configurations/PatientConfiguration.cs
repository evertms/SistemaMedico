using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Infrastructure.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.MedicalRecord)
            .WithOne(r => r.Patient)
            .HasForeignKey<MedicalRecord>(r => r.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId);

        builder.HasMany(p => p.MedicalNotes)
            .WithOne(n => n.Patient)
            .HasForeignKey(n => n.PatientId);

        builder.HasMany(p => p.Diagnoses)
            .WithOne(d => d.Patient)
            .HasForeignKey(d => d.PatientId);
    }
}
