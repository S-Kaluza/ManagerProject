using Application.Domains;

namespace Domain.Tasks.Commands.UpdateTaskHandler.Request;

public class UpdateTaskRequest
{
    public int CurrentUserId { get; set; }
    public int TaskId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? TasksStatusId { get; set; }
    
    public void Validate()
    {
        var validator = new UpdateTaskRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}