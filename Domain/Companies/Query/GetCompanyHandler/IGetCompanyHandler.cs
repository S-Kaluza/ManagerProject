using Domain.Companies.Query.GetCompanyHandler.Request;

namespace Domain.Companies.Query.GetCompanyHandler;

public interface IGetCompanyHandler
{
    Task<GetCompanyResponseAdmin> Handle(GetCompanyRequest request);
}