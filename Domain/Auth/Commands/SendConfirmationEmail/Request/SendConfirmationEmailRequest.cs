using Application.Domains;

namespace Domain.Auth.Commands.SendConfirmationEmail.Request;

public class SendConfirmationEmailRequest
{
    public string Email { get; set; }

    public void Validate()
    {
        var validator = new SendConfirmationEmailRequestValidator();
        
        var validationResult = validator.Validate(this);

        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}