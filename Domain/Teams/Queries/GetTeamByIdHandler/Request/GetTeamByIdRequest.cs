using Application.Domains;

namespace Domain.Teams.Queries.GetTeamByIdHandler.Request;

public class GetTeamByIdRequest
{
    public int CurrentUserId { get; set; }
    public int TeamId { get; set; }
    
    public void Validate()
    {
        var validator = new GetTeamByIdRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}