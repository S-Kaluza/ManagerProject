using Application.Domains;

namespace Domain.Tasks.Queries.GetTaskByIdHandler.Request;

public class GetTaskByIdRequest
{
    public int CurrentUserId { get; set; }
    public int TaskId { get; set; }
    
    public void Validate()
    {
        var validator = new GetTaskByIdRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}