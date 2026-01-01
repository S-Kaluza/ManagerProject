using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Enums;
using Application.Models.Entity;
using Microsoft.IdentityModel.Tokens;

namespace TokenService;

public class JwtTokenService : IJwtTokenService
{
    public async Task<AuthToken> Handle(User user, string secretKey, int expirationHours,
        List<Claim> privilegeClaims)
    {
        var expiresTime = DateTime.UtcNow.Add(TimeSpan.FromHours(24));
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.Role, Enum.GetName(typeof(RolesEnum), user.RolesId)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
        };

        var allUserClaims = claims.Concat(privilegeClaims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(allUserClaims),
            Expires = expiresTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return new AuthToken
        {
            Token = jwtToken,
            Expires = expiresTime
        };
    }

    public TokenValidationParameters GetTokenValidationParameters(string issuerSigningKey, int expirationTime)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(issuerSigningKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            LifetimeValidator = LifetimeValidator,
            ClockSkew = TimeSpan.FromMinutes(expirationTime),
            RoleClaimType = ClaimTypes.Role
        };
    }

    private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
        TokenValidationParameters validationParameters)
    {
        if (expires != null)
            if (DateTime.UtcNow < expires)
                return true;
        return false;
    }
}