using DataAccess;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GryphonProject.EndpointDefinitions;

public static class MedievalCityBuilderExtension
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(cs));
        builder.Services.AddTransient<NpgsqlConnection>(_ =>
            new NpgsqlConnection()
        );
    }

    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(Program).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();
        foreach (var endpointDefinition in endpointDefinitions) endpointDefinition.RegisterEndpoints(app);
    }
}