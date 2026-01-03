using FluentValidation;

namespace Application.Models.Validators;

public class PasswordValidator: AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x).NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]+").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\!\?\*\.]").WithMessage("Password must contain at least one special character.");
    }
}