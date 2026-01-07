using FluentValidation;

namespace Domain.Teams.Queries.GetTeamByIdHandler.Request;

public class GetTeamByIdRequestValidator : AbstractValidator<GetTeamByIdRequest>
{
    public GetTeamByIdRequestValidator()
    {
        RuleFor(x => x.TeamId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
    }
}