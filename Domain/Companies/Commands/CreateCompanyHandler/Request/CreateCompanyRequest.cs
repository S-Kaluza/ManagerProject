using Application.Domains;

namespace Domain.Companies.Commands.CreateCompanyHandler.Request;

public class CreateCompanyRequest
{
    public string Name { get; set; }
    
    public int CurrentUserId { get; set; }
    public string? Description { get; set; }
    
    public void Validate()
    {
        var validator = new CreateCompanyRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}