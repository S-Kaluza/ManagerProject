using Domain.Teams.Commands.DeleteTeamByIdHandler.Request;

namespace Domain.Teams.Commands.DeleteTeamByIdHandler;

public interface IDeleteTeamHandler
{
    Task<DeleteTeamResponse> Handle(DeleteTeamRequest request);
}