using Application.Domains;

namespace Domain.Tasks.Queries.GetTasksByUserIdHandler.Request;

public class GetTasksByUserIdRequest
{
    public int CurrentUserId { get; set; }
    public int UserId { get; set; }
    
    public void Validate()
    {
        var validator = new GetTasksByUserIdRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}