using FluentValidation;

namespace Domain.Companies.Commands.UpadateCompanyHandler.Request;

public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyRequestValidator()
    {
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(250); 
    }
}