using FluentValidation;

namespace Domain.Companies.Commands.DeleteCompanyHandler.Request;

public class DeleteCompanyRequestValidator : AbstractValidator<DeleteCompanyRequest>
{
    public DeleteCompanyRequestValidator()
    {
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
    }
}