using Application.Domains;

namespace Domain.Auth.Commands.AccountLoginHandler.Request;

public class AccountLoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public void Validate()
    {
        var validator = new AccountLoginRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}