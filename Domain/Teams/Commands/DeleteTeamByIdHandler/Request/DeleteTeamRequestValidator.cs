using FluentValidation;

namespace Domain.Teams.Commands.DeleteTeamByIdHandler.Request;

public class DeleteTeamRequestValidator : AbstractValidator<DeleteTeamRequest>
{
    public DeleteTeamRequestValidator()
    {
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
    }
}