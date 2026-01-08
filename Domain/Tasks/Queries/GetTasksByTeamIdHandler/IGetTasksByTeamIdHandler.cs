using Domain.Tasks.Queries.GetTasksByTeamIdHandler.Request;

namespace Domain.Tasks.Queries.GetTasksByTeamIdHandler;

public interface IGetTasksByTeamIdHandler
{
    Task<GetTasksByTeamIdResponse> Handle(GetTasksByTeamIdRequest request);
}