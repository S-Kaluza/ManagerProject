using Domain.Teams.Commands.CreateTeamHandler.Request;

namespace Domain.Teams.Commands.CreateTeamHandler;

public interface ICreateTeamHandler
{
    Task<CreateTeamResponse> Handle(CreateTeamRequest request);
}