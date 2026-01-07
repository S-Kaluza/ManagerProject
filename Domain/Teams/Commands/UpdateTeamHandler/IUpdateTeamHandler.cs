using Domain.Teams.Commands.UpdateTeamHandler.Request;

namespace Domain.Teams.Commands.UpdateTeamHandler;

public interface IUpdateTeamHandler
{
    Task<UpdateTeamResponse> Handle(UpdateTeamRequest request);
}