using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProjDependencyInjection.Extensions;

public static class DataAccessConfigExtension
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration,
        bool debugLogging = false)
    {
        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetSection("ConnectionString").Value,
                x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }
}