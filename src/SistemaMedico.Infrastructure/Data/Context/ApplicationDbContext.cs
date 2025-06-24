using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Infrastructure.Data.Configurations;

namespace SistemaMedico.Infrastructure.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<MedicalAppointment> MedicalAppointments { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<MedicalNote> MedicalNotes { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<AvailableDoctorSchedule> AvailableDoctorSchedules { get; set; }
    public DbSet<Specialty> Specialties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AvailableDoctorScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new DiagnosisConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new MedicalNoteConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
