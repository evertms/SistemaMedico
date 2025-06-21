using Microsoft.Extensions.DependencyInjection;
using SistemaMedico.Domain.Repositories;
using SistemaMedico.Infrastructure.Repositories;

namespace SistemaMedico.Infrastructure.Extensions;

// Pacotes necesarios
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Registrar configuración
        //var supabaseSettings = configuration.GetSection("Supabase").Get<SupabaseSettings>();
        //services.AddSingleton<ISupabaseSettings>(supabaseSettings);

        // Registrar cliente de Supabase
        //services.AddSingleton<ISupabaseClient, SupabaseClientWrapper>();

        // Registrar repositorios
        //services.AddScoped<IPatientRepository, PacienteRepository>();
        services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();

        // Otros servicios
        //services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}