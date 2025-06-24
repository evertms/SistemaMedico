using Supabase;
namespace SistemaMedico.Infrastructure.Data;

public class SupabaseClientFactory : ISupabaseClientFactory
{
    private readonly string _url;
    private readonly string _anonKey;

    public SupabaseClientFactory(string url, string anonKey)
    {
        _url = url;
        _anonKey = anonKey;
    }

    public Client CreateClient()
    {
        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        };
        return new Client(_url, _anonKey, options);
    }
}