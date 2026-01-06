using FluentValidation;

namespace Domain.Companies.Query.GetCompanyHandler.Request;

public class GetCompanyRequestValidator: AbstractValidator<GetCompanyRequest>
{
    public GetCompanyRequestValidator()
    {
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
    }
}