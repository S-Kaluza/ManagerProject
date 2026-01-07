using FluentValidation;

namespace Domain.Teams.Commands.CreateTeamHandler.Request;

public class CreateTeamRequestValidator : AbstractValidator<CreateTeamRequest>
{
    public CreateTeamRequestValidator()
    {
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }
}