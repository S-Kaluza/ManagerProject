using Domain.Teams.Commands.InviteUserToTeamHandler.Request;

namespace Domain.Teams.Commands.InviteUserToTeamHandler;

public interface IInviteUserToTeamHandler
{
    Task<InviteUserToTeamResponse> Handle(InviteUserToTeamRequest request);
}