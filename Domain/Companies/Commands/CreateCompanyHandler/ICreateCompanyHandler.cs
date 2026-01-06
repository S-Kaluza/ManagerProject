using Domain.Companies.Commands.CreateCompanyHandler.Request;

namespace Domain.Companies.Commands.CreateCompanyHandler;

public interface ICreateCompanyHandler
{
    Task<CreateCompanyResponse> Handle(CreateCompanyRequest request);
}