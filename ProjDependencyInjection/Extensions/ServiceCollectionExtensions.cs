using System.IdentityModel.Tokens.Jwt;
using Application.General;
using Application.Models.Entity;
using Application.Models.Entity;
using DataAccess;
using ProjDependencyInjection.ConfigureOptions;
using TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProjDependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureCommonServices(this IServiceCollection service, IConfiguration configuration,
        string projectName)
    {
        service.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                //options.Tokens.EmailConfirmationTokenProvider = "EmailDataProtectorTokenProvider";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        service.AddRouting();

        service.AddCustomAuthorization();

        service.ConfigureOptions<ConfigureJwtBearear>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        service.AddSettingsConfig(configuration);

        service.AddTransient<IJwtTokenService, JwtTokenService>();

        service.AddHttpContextAccessor();
        service.AddCors();
        service.AddMapster();
        service.AddControllers();
        service.AddEndpointsApiExplorer();
        service.AddSwagger(projectName);
        service.AddDataAccess(configuration);
    }
}