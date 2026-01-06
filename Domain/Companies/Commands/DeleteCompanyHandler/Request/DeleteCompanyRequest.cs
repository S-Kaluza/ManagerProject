using Application.Domains;

namespace Domain.Companies.Commands.DeleteCompanyHandler.Request;

public class DeleteCompanyRequest
{
    public int CurrentUserId { get; set; }
    
    public void Validate()
    {
        var validator = new DeleteCompanyRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}