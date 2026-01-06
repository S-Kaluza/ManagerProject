using Application.Domains;

namespace Domain.Companies.Commands.UpadateCompanyHandler.Request;

public class UpdateCompanyRequest
{
    public int CurrentUserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public void Validate()
    {
        var validator = new UpdateCompanyRequestValidator();
        
        var validationResult = validator.Validate(this);

        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}