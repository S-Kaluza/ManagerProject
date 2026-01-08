namespace Domain.Teams.Commands.InviteUserToTeamHandler.Request;

public class InviteUserToTeamRequest
{
    public int CurrentUserId { get; set; }
    public int TeamId { get; set; }
    public int UserId { get; set; }
}