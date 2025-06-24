using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Infrastructure.Data.Configurations;

public class AvailableDoctorScheduleConfiguration : IEntityTypeConfiguration<AvailableDoctorSchedule>
{
    public void Configure(EntityTypeBuilder<AvailableDoctorSchedule> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Doctor)
            .WithMany(d => d.Schedules)
            .HasForeignKey(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
