using FluentValidation;

namespace Domain.Tasks.Commands.CreateTaskHandler.Request;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(1000).NotEmpty();
        RuleFor(x => x.TeamId).GreaterThan(0);
        RuleFor(x => x.CurrentUserId).GreaterThan(0);
    }
}