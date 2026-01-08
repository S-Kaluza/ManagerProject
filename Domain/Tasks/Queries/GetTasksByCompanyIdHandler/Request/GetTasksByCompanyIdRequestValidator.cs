using FluentValidation;

namespace Domain.Tasks.Queries.GetTasksByCompanyIdHandler.Request;

public class GetTasksByCompanyIdRequestValidator: AbstractValidator<GetTasksByCompanyIdRequest>
{
    public GetTasksByCompanyIdRequestValidator()
    {
        RuleFor(x => x.CompanyId).GreaterThan(0);
        RuleFor(x => x.CurrentUserId).GreaterThan(0);       
    }
}