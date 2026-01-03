using FluentValidation;

namespace Domain.Auth.Commands.SendConfirmationEmail.Request;

public class SendConfirmationEmailRequestValidator : AbstractValidator<SendConfirmationEmailRequest>
{
    public SendConfirmationEmailRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}