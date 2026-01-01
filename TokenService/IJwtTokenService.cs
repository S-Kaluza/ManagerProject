using System.Security.Claims;
using Application.Models.Entity;
using TokenService;
using Microsoft.IdentityModel.Tokens;

namespace TokenService;

public interface IJwtTokenService
{
    Task<AuthToken> Handle(User user, string secretKey, int expirationHours, List<Claim> claims);
    public TokenValidationParameters GetTokenValidationParameters(string issuerSigningKey, int expirationTime);
}