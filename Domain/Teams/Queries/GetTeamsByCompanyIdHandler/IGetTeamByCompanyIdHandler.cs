using Domain.Teams.Queries.GetTeamsByCompanyIdHandler.Request;

namespace Domain.Teams.Queries.GetTeamsByCompanyIdHandler;

public interface IGetTeamsByCompanyIdHandler
{
    Task<GetTeamsByCompanyIdResponse> Handle(GetTeamsByCompanyIdRequest request);
}