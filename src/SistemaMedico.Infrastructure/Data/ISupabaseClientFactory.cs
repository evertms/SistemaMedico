using SistemaMedico.Infrastructure.Services;

namespace SistemaMedico.Infrastructure.Data;

public interface ISupabaseClientFactory
{
    Supabase.Client CreateClient();
}