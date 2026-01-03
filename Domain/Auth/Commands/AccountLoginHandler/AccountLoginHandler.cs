using System.Security.Claims;
using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.Entity;
using Application.Models.Settings;
using DataAccess;
using Domain.Auth.Commands.AccountLoginHandler.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TokenService;

namespace Domain.Auth.Commands.AccountLoginHandler;

public class AccountLoginHandler : IAccountLoginHandler
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly SecuritySettings _securitySettings;
    
    
    public AccountLoginHandler(ApplicationDbContext context,
                               UserManager<User> userManager,
                               IOptions<SecuritySettings> settings,
                               IJwtTokenService jwtTokenService)
    {
        _context = context;
        _userManager = userManager;
        _securitySettings = settings.Value;
        _jwtTokenService = jwtTokenService;
    }
    
    public async Task<AccountLoginResponse> Handle(AccountLoginRequest request)
    {
        request.Validate();

        var normalizedEmailFromRequest = _userManager.NormalizeEmail(request.Email);

        var user = await _context.Users
                       .FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedEmailFromRequest
                                            && x.StatusId == (int)StatusEnum.Active)
                   ?? throw new DomainException(ErrorCodeEnum.UserNotFound,
                       ErrorCodeEnum.UserNotFound.GetDescription());

        if (!user.EmailConfirmed)
        {
            throw new DomainException(ErrorCodeEnum.UserEmailIsNotConfirmed, ErrorCodeEnum.UserEmailIsNotConfirmed.GetDescription());
        }

        var result = _userManager.CheckPasswordAsync(user, request.Password);

        if (!result.Result)
        {
            throw new DomainException(ErrorCodeEnum.InvalidCredentials, ErrorCodeEnum.InvalidCredentials.GetDescription());
        }

        AuthToken token = await _jwtTokenService.Handle(user, _securitySettings.JwtSettings.IssuerSigningKey,
            _securitySettings.JwtSettings.ExpirationTime, new List<Claim>());

        return new()
        {
            Token = token.Token,
            Expires = token.Expires
        };
    }
}