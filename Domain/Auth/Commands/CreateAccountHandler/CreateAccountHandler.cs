using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.Entity;
using Application.Models.Settings;
using DataAccess;
using Domain.Auth.Commands.CreateAccountHandler.Request;
using Domain.Auth.Commands.SendConfirmationEmail;
using Domain.Auth.Commands.SendConfirmationEmail.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TokenService;

namespace Domain.Auth.Commands.CreateAccountHandler;

public class CreateAccountHandler : ICreateAccountHandler
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly SecuritySettings _securitySettings;
    private readonly ISendConfirmationEmailRequestHandler _sendConfirmationEmailHandler;
    
    public CreateAccountHandler(ApplicationDbContext context,
                                UserManager<User> userManager,
                                IOptions<SecuritySettings> settings,
                                IJwtTokenService jwtTokenService,
                                ISendConfirmationEmailRequestHandler sendConfirmationEmailHandler)
    {
        _context = context;
        _userManager = userManager;
        _securitySettings = settings.Value;
        _jwtTokenService = jwtTokenService;
        _sendConfirmationEmailHandler = sendConfirmationEmailHandler;
    }
    
    public async Task<CreateAccountRequestResponse> Handle(CreateAccountRequest request)
    {
        request.Validate();
        Random rnd = new Random();

        var normalizedEmailFromRequest = _userManager.NormalizeEmail(request.Email);

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedEmailFromRequest);

        if (user is not null)
        {
            throw new DomainException(ErrorCodeEnum.UserAlreadyExists,
                ErrorCodeEnum.UserAlreadyExists.GetDescription());
        }

        User newUser = new()
        {
            Email = request.Email,
            NormalizedEmail = normalizedEmailFromRequest,
            UserName = request.Name.RemoveDiacritics().ToLower() + request.Surname.RemoveDiacritics().ToLower() + rnd.Next(100000, 999999),
            Name = request.Name,
            Surname = request.Surname,
            StatusId = (int)StatusEnum.Inactive,
            RolesId = (int)RolesEnum.User,
            CreationDate = DateTime.Now,
        };

        await using var transaction = await _context.Database.BeginTransactionAsync();

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            throw new DomainException(ErrorCodeEnum.GeneralError, ErrorCodeEnum.GeneralError.GetDescription());
        }

        var sendConfirmationEmailRequest = new SendConfirmationEmailRequest()
        {
            Email = request.Email
        };

        // await _sendConfirmationEmailHandler.Handle(sendConfirmationEmailRequest);
        await transaction.CommitAsync();
        return new()
        {
            Message = "User created successfully",
        };
    }
}