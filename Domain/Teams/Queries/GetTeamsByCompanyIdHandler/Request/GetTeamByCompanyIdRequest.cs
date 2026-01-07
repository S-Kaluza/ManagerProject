using Application.Domains;

namespace Domain.Teams.Queries.GetTeamsByCompanyIdHandler.Request;

public class GetTeamsByCompanyIdRequest
{
    public int CurrentUserId { get; set; }
    public int CompanyId { get; set; }
    
    public void Validate()
    {
        var validator = new GetTeamsByCompanyIdRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}