using System.Security.Claims;
using Application.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ProjDependencyInjection.Extensions;

public static class AuthorizationExtension
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("SuperAdmin", policy => policy.RequireAssertion(ctx =>
                ctx.User.Claims.Any(x =>
                    x.Type == ClaimTypes.Role && Enum.TryParse<RolesEnum>(x.Value, out var role) &&
                    role == RolesEnum.SuperAdmin)));

            options.AddPolicy("Admin", policy => policy.RequireAssertion(ctx =>
                ctx.User.Claims.Any(x =>
                    x.Type == ClaimTypes.Role && Enum.TryParse<RolesEnum>(x.Value, out var role) &&
                    (role == RolesEnum.Admin || role == RolesEnum.SuperAdmin))));
        });
        return services;
    }
}