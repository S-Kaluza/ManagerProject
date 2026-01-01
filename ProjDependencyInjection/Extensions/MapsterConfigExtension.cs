using ProjDependencyInjection.Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ProjDependencyInjection.Extensions;

public static class MapsterConfigExtensions
{
    public static void AddMapster(this IServiceCollection services)
    {
        services.AddSingleton<IMapsterConfiguration, MapsterConfiguration>();
    }
}