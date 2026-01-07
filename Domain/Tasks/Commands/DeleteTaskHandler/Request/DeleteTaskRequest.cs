using Application.Domains;

namespace Domain.Tasks.Commands.DeleteTaskHandler.Request;

public class DeleteTaskRequest
{
    public int currentUserId { get; set; }
    public int taskId { get; set; }
    
    public void Validate()
    {
        var validator = new DeleteTaskRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}