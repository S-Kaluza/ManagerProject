using FluentValidation;

namespace Domain.Tasks.Commands.DeleteTaskHandler.Request;

public class DeleteTaskRequestValidator: AbstractValidator<DeleteTaskRequest>
{
    public DeleteTaskRequestValidator()
    {
        RuleFor(x => x.currentUserId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.taskId).GreaterThan(0).NotNull().NotEmpty();
    }
}