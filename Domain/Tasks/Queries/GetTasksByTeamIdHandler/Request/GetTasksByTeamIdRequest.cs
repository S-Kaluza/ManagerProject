using Application.Domains;

namespace Domain.Tasks.Queries.GetTasksByTeamIdHandler.Request;

public class GetTasksByTeamIdRequest
{
    public int TeamId { get; set; }
    public int CurrentUserId { get; set; }
    
    public void Validate()
    {
        var validator = new GetTasksByTeamIdRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}