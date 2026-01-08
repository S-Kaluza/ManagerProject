using FluentValidation;

namespace Domain.Tasks.Queries.GetTaskByIdHandler.Request;

public class GetTaskByIdRequestValidator: AbstractValidator<GetTaskByIdRequest>
{
    public GetTaskByIdRequestValidator()
    {
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.TaskId).GreaterThan(0).NotNull().NotEmpty();
    }
}