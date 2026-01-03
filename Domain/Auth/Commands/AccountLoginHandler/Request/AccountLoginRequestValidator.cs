using Application.Models.Validators;
using FluentValidation;

namespace Domain.Auth.Commands.AccountLoginHandler.Request;

public class AccountLoginRequestValidator : AbstractValidator<AccountLoginRequest>
{
    public AccountLoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().SetValidator(new PasswordValidator());
    }
}