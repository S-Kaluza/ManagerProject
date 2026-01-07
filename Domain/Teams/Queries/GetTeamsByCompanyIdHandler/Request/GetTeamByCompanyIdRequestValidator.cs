using FluentValidation;

namespace Domain.Teams.Queries.GetTeamsByCompanyIdHandler.Request;

public class GetTeamsByCompanyIdRequestValidator : AbstractValidator<GetTeamsByCompanyIdRequest>
{
    public GetTeamsByCompanyIdRequestValidator()
    {
        RuleFor(x => x.CompanyId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
    }
}