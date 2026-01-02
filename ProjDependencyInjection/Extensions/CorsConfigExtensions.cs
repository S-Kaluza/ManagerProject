using Application.Models.Settings;
using Microsoft.AspNetCore.Builder;

namespace ProjDependencyInjection.Extensions;

public static class CorsConfigExtensions
{
    public static void UseCorsConfig(this IApplicationBuilder app, SecuritySettings securitySettings)
    {
        if (securitySettings == null)
            throw new ArgumentNullException(nameof(securitySettings), "SecuritySettings is null!");

        if (string.IsNullOrEmpty(securitySettings.CORSOrigin))
            throw new ArgumentException("CORSOrigin is null or empty!", nameof(securitySettings));
        
        app.UseCors(options =>
        {
            if (securitySettings.CORSOrigin == "*")
            {
                options.AllowAnyOrigin();
            }
            else
            {
                options.WithOrigins(securitySettings.CORSOrigin);
                if (bool.Parse(securitySettings.CookieAllowCredentials)) options.AllowCredentials();
            }

            options.AllowAnyMethod();
            options.AllowAnyHeader();
            options.WithExposedHeaders("Content-Disposition");
        });
    }
}