using Application.Domains;

namespace Domain.Auth.Profile.Query.GetUserByIdHandler.Request;

public class GetUserByIdRequest
{
    public int CurrentUserId { get; set; }
    public int UserId { get; set; }
    
    public void Validate()
    {
        var validator = new GetUserByIdRequestValidator();
        var validationResult = validator.Validate(this);
        if (validationResult.Errors.Count > 0)
        {
            throw new DomainException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}