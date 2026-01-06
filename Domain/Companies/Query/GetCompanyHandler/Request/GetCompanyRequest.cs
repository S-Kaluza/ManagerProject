using Application.Domains;

namespace Domain.Companies.Query.GetCompanyHandler.Request;

public class GetCompanyRequest
{
    public int CurrentUserId { get; set; }
    
    public void Validate()
    {
        var validator = new GetCompanyRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}