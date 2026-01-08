using FluentValidation;

namespace Domain.Tasks.Queries.GetTasksByUserIdHandler.Request;

public class GetTasksByUserIdRequestValidator: AbstractValidator<GetTasksByUserIdRequest>
{
    public GetTasksByUserIdRequestValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);       
        RuleFor(x => x.CurrentUserId).GreaterThan(0);      
    }
}