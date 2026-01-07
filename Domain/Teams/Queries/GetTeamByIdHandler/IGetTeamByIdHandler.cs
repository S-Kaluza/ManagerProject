using Domain.Teams.Queries.GetTeamByIdHandler.Request;

namespace Domain.Teams.Queries.GetTeamByIdHandler;

public interface IGetTeamByIdHandler
{
    Task<GetTeamByIdResponse> Handle(GetTeamByIdRequest request);
}