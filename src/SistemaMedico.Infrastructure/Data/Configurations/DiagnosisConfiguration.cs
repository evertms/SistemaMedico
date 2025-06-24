using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Infrastructure.Data.Configurations;

public class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
{
    public void Configure(EntityTypeBuilder<Diagnosis> builder)
    {
        builder.HasKey(d => d.Id);

        builder
            .HasOne(d => d.MedicalNote)
            .WithOne(n => n.Diagnosis)
            .HasForeignKey<Diagnosis>(d => d.MedicalNoteId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
