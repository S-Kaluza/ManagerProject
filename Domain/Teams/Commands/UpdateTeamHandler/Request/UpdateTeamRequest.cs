using Application.Domains;

namespace Domain.Teams.Commands.UpdateTeamHandler.Request;

public class UpdateTeamRequest
{
    public int TeamId { get; set; }
    public int CurrentUserId { get; set; }
    public string Name { get; set; }
    
    public void Validate()
    {
        var validator = new UpdateTeamRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}