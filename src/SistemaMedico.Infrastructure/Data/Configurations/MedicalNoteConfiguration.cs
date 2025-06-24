using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Infrastructure.Data.Configurations;

public class MedicalNoteConfiguration : IEntityTypeConfiguration<MedicalNote>
{
    public void Configure(EntityTypeBuilder<MedicalNote> builder)
    {
        builder.HasKey(n => n.Id);

        builder
            .HasOne(n => n.Appointment)
            .WithOne(a => a.MedicalNote)
            .HasForeignKey<MedicalNote>(n => n.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull); // o Restrict
    }

}