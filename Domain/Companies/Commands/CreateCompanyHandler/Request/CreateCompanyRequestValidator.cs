using FluentValidation;

namespace Domain.Companies.Commands.CreateCompanyHandler.Request;

public class CreateCompanyRequestValidator: AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
    {
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(250);
    }
}