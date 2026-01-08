using FluentValidation;

namespace Domain.Teams.Commands.InviteUserToTeamHandler.Request;

public class InviteUserToTeamRequestValidator : AbstractValidator<InviteUserToTeamRequest>
{
    public  InviteUserToTeamRequestValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.TeamId).GreaterThan(0);
        RuleFor(x => x.CurrentUserId).GreaterThan(0);
    }
}