using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Domains;
using Application.Enums;


namespace Application.Extensions;

public static class ClaimsPrincipalExtension
{
    public static int GetUserIdFromClaims(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;
        if (claim == null || string.IsNullOrEmpty(claim))
        {
            throw new DomainException(ErrorCodeEnum.UserNotFound, ErrorCodeEnum.UserNotFound.GetDescription());
        }
        return int.Parse(claim);
    }
}