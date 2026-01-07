using FluentValidation;

namespace Domain.Tasks.Commands.UpdateTaskHandler.Request;

public class UpdateTaskRequestValidator: AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.TaskId).GreaterThan(0).NotNull().NotEmpty();
    }
}