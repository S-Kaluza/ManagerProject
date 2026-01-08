using Application.Domains;

namespace Domain.Tasks.Queries.GetTasksByCompanyIdHandler.Request;

public class GetTasksByCompanyIdRequest
{
    public int CompanyId { get; set; }
    public int CurrentUserId { get; set; }
    
    public void Validate()
    {
        var validator = new GetTasksByCompanyIdRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}