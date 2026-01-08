using FluentValidation;

namespace Domain.Tasks.Queries.GetTasksByTeamIdHandler.Request;

public class GetTasksByTeamIdRequestValidator : AbstractValidator<GetTasksByTeamIdRequest>
{
    public GetTasksByTeamIdRequestValidator()
    {
        RuleFor(x => x.TeamId).GreaterThan(0);       
        RuleFor(x => x.CurrentUserId).GreaterThan(0);      
    }
}