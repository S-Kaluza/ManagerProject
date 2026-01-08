using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Teams.Commands.InviteUserToTeamHandler.Request;
using Microsoft.EntityFrameworkCore;

namespace Domain.Teams.Commands.InviteUserToTeamHandler;

public class InviteUserToTeamHandler : IInviteUserToTeamHandler
{
    private readonly ApplicationDbContext _context;
    
    public InviteUserToTeamHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<InviteUserToTeamResponse> Handle(InviteUserToTeamRequest request)
    {
        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());
        
        var userToInvite = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.UserId && x.CompanyId == currentUser.CompanyId &&x.StatusId == (int)StatusEnum.Active)
                          ?? throw new DomainException(ErrorCodeEnum.UserNotFound,
                              ErrorCodeEnum.UserNotFound.GetDescription());
        
        var team = await _context.Teams
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == request.TeamId && x.CompanyId == currentUser.CompanyId)
            ?? throw new DomainException(ErrorCodeEnum.TeamNotFound, ErrorCodeEnum.TeamNotFound.GetDescription());

        if (team.Users.Any(u => u.Id == userToInvite.Id))
        {
            throw new DomainException(ErrorCodeEnum.UserAlreadyAssignedToTeam,
                ErrorCodeEnum.UserAlreadyAssignedToTeam.GetDescription());
        }

        team.AddMember(userToInvite);
        
        await _context.SaveChangesAsync();
        
        return new InviteUserToTeamResponse()
        {
            TeamId = team.Id,
            UserId = userToInvite.Id
        };
    }
}