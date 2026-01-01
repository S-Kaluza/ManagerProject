using System.Net;
using System.Text;
using System.Text.Json;
using Application.Models.Settings;
using TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ProjDependencyInjection.ConfigureOptions;

public class ConfigureJwtBearear : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly SecuritySettings _securitySettings;
    
    public ConfigureJwtBearear(IOptions<SecuritySettings> settings, IJwtTokenService tokenService)
    {
        _jwtTokenService = tokenService;
        _securitySettings = settings.Value;
    }
    
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = _jwtTokenService.GetTokenValidationParameters(
            _securitySettings.JwtSettings.IssuerSigningKey, _securitySettings.JwtSettings.ExpirationTime);
        options.Events = new JwtBearerEvents
        {
            OnChallenge = OnChallenge
        };
    }
    
    private async Task OnChallenge(JwtBearerChallengeContext context)
    {
        if (context.AuthenticateFailure?.GetType() == typeof(SecurityTokenInvalidLifetimeException))
        {
            context.HandleResponse();
            var json = JsonSerializer.Serialize(new { message = "Token Expired", code = -1 });
            var data = Encoding.UTF8.GetBytes(json);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.Body.WriteAsync(data);
        }
    }
}