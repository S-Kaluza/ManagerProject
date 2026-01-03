using Application.Domains;

namespace Domain.Auth.Commands.CreateAccountHandler.Request;

public class CreateAccountRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public void Validate()
    {
        var validator = new CreateAccountRequestValidator();
        
        var validationResult = validator.Validate(this);

        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}