using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Helpers.Email;
using Application.Models.Entity;
using Application.Models.Settings;
using DataAccess;
using Domain.Auth.Commands.SendConfirmationEmail.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Task = System.Threading.Tasks.Task;

namespace Domain.Auth.Commands.SendConfirmationEmail;

public class SendConfirmationEmailRequestHandler : ISendConfirmationEmailRequestHandler
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly EmailSettings _emailSettings;
    
    public SendConfirmationEmailRequestHandler(
        ApplicationDbContext context, 
        UserManager<User> userManager, 
        IOptions<EmailSettings> emailSettings)
    {
        _context = context;
        _userManager = userManager;
        _emailSettings = emailSettings.Value;
    }
    
    public async Task Handle(SendConfirmationEmailRequest request)
    {
        request.Validate();
        
        var normalizedEmailFromRequest = _userManager.NormalizeEmail(request.Email);

        var user = _context.Users.FirstOrDefault(x => x.NormalizedEmail == normalizedEmailFromRequest)
                   ?? throw new DomainException(ErrorCodeEnum.UserNotFound,
                       ErrorCodeEnum.UserNotFound.GetDescription());

        if (user.EmailConfirmed)
        {
            throw new DomainException(ErrorCodeEnum.UserEmailAlreadyConfirmed,
                ErrorCodeEnum.UserEmailAlreadyConfirmed.GetDescription());
        }
        
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        if (token == null)
        {
            throw new DomainException(ErrorCodeEnum.TokenNotGenerated, ErrorCodeEnum.TokenNotGenerated.GetDescription());
        }

        string linkRoute = "confirm-email"; 

        var body = GenerateEmailConfirmationBody(
            _emailSettings.FrontendUrl, 
            linkRoute, 
            token, 
            user.Email
        );

        var subject = "Confirm Email";
        
        await EmailHelper.SendEmail(request.Email, _emailSettings, body, subject);

        await Task.CompletedTask;
    }

    private static string GenerateEmailConfirmationBody(string frontendUrl, string linkRoute, string token, string email)
    {
        var encodedToken = Uri.EscapeDataString(token);
        var encodedEmail = Uri.EscapeDataString(email);
        
        var baseUrl = frontendUrl.TrimEnd('/');
        var path = linkRoute.TrimStart('/');

        var fullUrl = $"{baseUrl}/{path}?token={encodedToken}&email={encodedEmail}";
        
        var html = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333333; }}
                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 5px; }}
                .header {{ text-align: center; margin-bottom: 20px; }}
                .button {{ display: inline-block; padding: 10px 20px; background-color: #007BFF; color: #ffffff; text-decoration: none; border-radius: 5px; font-weight: bold; }}
                .footer {{ margin-top: 30px; font-size: 12px; color: #777777; text-align: center; }}
                a {{ color: #007BFF; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h2>Witaj w naszej aplikacji!</h2>
                </div>
                <p>Cześć,</p>
                <p>Dziękujemy za rejestrację. Aby aktywować swoje konto, kliknij poniższy przycisk:</p>
                
                <p style='text-align: center; margin: 30px 0;'>
                    <a href='{fullUrl}' class='button'>Potwierdź adres e-mail</a>
                </p>
                
                <p>Jeśli przycisk nie działa, skopiuj i wklej poniższy link do przeglądarki:</p>
                <p><a href='{fullUrl}'>{fullUrl}</a></p>
                
                <div class='footer'>
                    <p>Jeśli nie zakładałeś konta w naszym serwisie, zignoruj tę wiadomość.</p>
                    <p>&copy; {DateTime.Now.Year} Twoja Firma</p>
                </div>
            </div>
        </body>
        </html>";

        return html;
    }
}