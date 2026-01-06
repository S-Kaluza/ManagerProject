using Domain.Companies.Commands.UpadateCompanyHandler.Request;

namespace Domain.Companies.Commands.UpadateCompanyHandler;

public interface IUpdateCompanyHandler
{
    Task<UpdateCompanyResponse> Handle(UpdateCompanyRequest request);
}