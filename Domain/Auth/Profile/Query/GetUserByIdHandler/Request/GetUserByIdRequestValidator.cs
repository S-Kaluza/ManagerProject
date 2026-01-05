using FluentValidation;

namespace Domain.Auth.Profile.Query.GetUserByIdHandler.Request;

public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdRequestValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).NotNull().NotEmpty();
        RuleFor(x => x.CurrentUserId).GreaterThan(0).NotNull().NotEmpty();
    }
}