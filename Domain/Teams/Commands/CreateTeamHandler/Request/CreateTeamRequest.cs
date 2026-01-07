using Application.Domains;

namespace Domain.Teams.Commands.CreateTeamHandler.Request;

public class CreateTeamRequest
{
    public string Name { get; set; }
    public int CurrentUserId { get; set; }
    
    public void Validate()
    {
        var validator = new CreateTeamRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}