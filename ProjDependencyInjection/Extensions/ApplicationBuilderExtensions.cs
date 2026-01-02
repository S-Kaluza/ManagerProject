using Application.Models.Settings;
using ProjDependencyInjection.Mapster;
using Microsoft.AspNetCore.Builder;

namespace ProjDependencyInjection.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureCommonPipeline(this IApplicationBuilder app, bool isDevelopment,
        MapsterConfiguration mapster, SecuritySettings securitySettings)
    {
        if (isDevelopment)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerConfig();
        }

        app.UseCorsConfig(securitySettings);
        mapster.Scan().Compile();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        if (!isDevelopment)
            app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}