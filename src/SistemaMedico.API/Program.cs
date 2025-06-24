using Microsoft.EntityFrameworkCore;
using SistemaMedico.Infrastructure.Data.Context;
using SistemaMedico.Infrastructure.Extensions;
using SistemaMedico.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor
builder.Services.AddControllers();

// Configura OpenAPI
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173") // URL del frontend
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configura el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    // Exponer el documento OpenAPI en /openapi/v1.json
    app.MapOpenApi("/openapi/v1.json");

    // Configura Swagger UI
    app.UseSwaggerUI(options =>
    {
        // Apunta a la ruta correcta del documento OpenAPI
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API v1");
        options.RoutePrefix = "docs"; // Acceso en /docs
        options.DocumentTitle = "Documentación de mi API";
        options.DefaultModelsExpandDepth(-1);
    });

    // Redirige desde la raíz (/) a /docs
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/docs");
        return Task.CompletedTask;
    });
}

// Habilita CORS
app.UseCors("AllowReact");

// Habilita autorización
app.UseAuthorization();

// Mapea los controladores
app.MapControllers();

// Ejecuta la aplicación
app.Run();