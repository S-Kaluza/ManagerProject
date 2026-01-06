using Domain.Companies.Commands.DeleteCompanyHandler.Request;

namespace Domain.Companies.Commands.DeleteCompanyHandler;

public interface IDeleteCompanyHandler
{
    Task<DeleteCompanyResponse> Handle(DeleteCompanyRequest request);
}