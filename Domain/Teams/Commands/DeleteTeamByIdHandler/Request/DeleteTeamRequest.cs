using Application.Domains;

namespace Domain.Teams.Commands.DeleteTeamByIdHandler.Request;

public class DeleteTeamRequest
{
    public int CurrentUserId { get; set; }
    public int TeamId { get; set; }
    
    public void Validate()
    {
        var validator = new DeleteTeamRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}