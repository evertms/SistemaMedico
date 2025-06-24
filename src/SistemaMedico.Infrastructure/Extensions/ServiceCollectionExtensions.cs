using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SistemaMedico.Application.Common.Interfaces.Services;
using SistemaMedico.Application.Common.Interfaces.UseCases;
using SistemaMedico.Application.UseCases.Appointments.CancelAppointment;
using SistemaMedico.Application.UseCases.Appointments.GetDoctorAppointments;
using SistemaMedico.Application.UseCases.Appointments.RescheduleAppointment;
using SistemaMedico.Application.UseCases.Appointments.ScheduleAppointment;
using SistemaMedico.Application.UseCases.Auth.Login;
using SistemaMedico.Application.UseCases.Doctors.AddDoctor;
using SistemaMedico.Application.UseCases.Doctors.DeactivateDoctor;
using SistemaMedico.Application.UseCases.Doctors.EditDoctor;
using SistemaMedico.Application.UseCases.Doctors.GetAvailableDoctors;
using SistemaMedico.Application.UseCases.Doctors.GetAvailableSchedules;
using SistemaMedico.Application.UseCases.MedicalRecords.CreateDiagnosis;
using SistemaMedico.Application.UseCases.MedicalRecords.CreateMedicalNote;
using SistemaMedico.Application.UseCases.MedicalRecords.DownloadMyMedicalRecord;
using SistemaMedico.Application.UseCases.MedicalRecords.GetMyMedicalRecord;
using SistemaMedico.Application.UseCases.MedicalRecords.GetPatientMedicalRecord;
using SistemaMedico.Application.UseCases.Notifications.SendAppointmentReminder;
using SistemaMedico.Application.UseCases.Patients.RegisterPatient;
using SistemaMedico.Application.UseCases.Patients.UnlinkPatient;
using SistemaMedico.Application.UseCases.Schedules.CreateDoctorAvailability;
using SistemaMedico.Application.UseCases.Specialties.GetAllSpecialties;
using SistemaMedico.Application.UseCases.Specialties.GetDoctorsBySpecialty;
using SistemaMedico.Application.UseCases.Users.RegisterUser;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Data;
using SistemaMedico.Infrastructure.Data.Configurations;
using SistemaMedico.Infrastructure.Data.Context;
using SistemaMedico.Infrastructure.Repositories;
using SistemaMedico.Infrastructure.Services;

namespace SistemaMedico.Infrastructure.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Registro de casos de uso
        services.AddScoped<IAddDoctorHandler, AddDoctorHandler>();
        services.AddScoped<ICancelAppointmentHandler, CancelAppointmentHandler>();
        services.AddScoped<ICreateDiagnosisHandler, CreateDiagnosisHandler>();
        services.AddScoped<ICreateDoctorAvailabilityHandler, CreateDoctorAvailabilityHandler>();
        services.AddScoped<ICreateMedicalNoteHandler, CreateMedicalNoteHandler>();
        services.AddScoped<IDeactivateDoctorHandler, DeactivateDoctorHandler>();
        services.AddScoped<IDownloadMyMedicalRecordHandler, DownloadMyMedicalRecordHandler>();
        services.AddScoped<IEditDoctorHandler, EditDoctorHandler>();
        services.AddScoped<IGetAllSpecialtiesHandler, GetAllSpecialtiesHandler>();
        services.AddScoped<IGetAvailableDoctorsHandler, GetAvailableDoctorsHandler>();
        services.AddScoped<IGetAvailableSchedulesHandler, GetAvailableSchedulesHandler>();
        services.AddScoped<IGetDoctorAppointmentsHandler, GetDoctorAppointmentsHandler>();
        services.AddScoped<IGetDoctorsBySpecialtyHandler, GetDoctorsBySpecialtyHandler>();
        services.AddScoped<IGetMyMedicalRecordHandler, GetMyMedicalRecordHandler>();
        services.AddScoped<IGetPatientMedicalRecordHandler, GetPatientMedicalRecordHandler>();
        services.AddScoped<ILoginHandler, LoginHandler>();
        services.AddScoped<IRegisterPatientHandler, RegisterPatientHandler>();
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        services.AddScoped<IRescheduleAppointmentHandler, RescheduleAppointmentHandler>();
        services.AddScoped<IScheduleAppointmentHandler, ScheduleAppointmentHandler>();
        services.AddScoped<ISendAppointmentReminderHandler, SendAppointmentReminderHandler>();
        services.AddScoped<IUnlinkPatientHandler, UnlinkPatientHandler>();
        
        // AppDbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        // Registrar configuración de Supabase
        services.Configure<SupabaseSettings>(configuration.GetSection("Supabase"));
        
        // Factory como singleton
        services.AddSingleton<ISupabaseClientFactory>(serviceProvider =>
        {
            var supabaseSettings = serviceProvider.GetRequiredService<IOptions<SupabaseSettings>>().Value;
            return new SupabaseClientFactory(supabaseSettings.Url, supabaseSettings.AnonKey);
        });
        
        // Client como Scoped
        services.AddScoped<Supabase.Client>(serviceProvider =>
        {
            var factory = serviceProvider.GetRequiredService<ISupabaseClientFactory>();
            return factory.CreateClient();
        });
        
        // Registrar repositorios
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>)); // Repositorio genérico
        services.AddScoped<IAvailableDoctorScheduleRepository, AvailableDoctorScheduleRepository>();
        services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();
        services.AddScoped<IMedicalNoteRepository, MedicalNoteRepository>();
        services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        // Registro de Servicios
        // Json web tokens
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
    
        services.AddScoped<ITokenService>(serviceProvider =>
        {
            var jwtSettings = serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;
        
            if (string.IsNullOrEmpty(jwtSettings.SecretKey))
                throw new InvalidOperationException("JWT Secret Key is required");
            
            return new JwtTokenService(jwtSettings.SecretKey);
        });
        
        // Password hasher
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        
        // Pdf generator
        services.AddScoped<IPdfGenerator, PdfSharpGenerator>();
        
        // Servicio de correo
        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
        services.AddTransient<IEmailService, EmailService>();
        
        // Servicio de reminder
        services.AddHostedService<AppointmentReminderBackgroundService>();
        return services;
    }
}