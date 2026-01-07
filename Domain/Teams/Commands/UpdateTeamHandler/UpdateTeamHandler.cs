using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Teams.Commands.UpdateTeamHandler.Request;
using Microsoft.EntityFrameworkCore;

namespace Domain.Teams.Commands.UpdateTeamHandler;

public class UpdateTeamHandler : IUpdateTeamHandler
{
    private readonly ApplicationDbContext _context;
    
    public UpdateTeamHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<UpdateTeamResponse> Handle(UpdateTeamRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());
        
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == request.TeamId && x.CompanyId == currentUser.CompanyId)
            ?? throw new DomainException(ErrorCodeEnum.CompanyNotFound, ErrorCodeEnum.CompanyNotFound.GetDescription());
        
        team.Name = request.Name;
        
        await _context.SaveChangesAsync();

        return new UpdateTeamResponse()
        {
            TeamId = team.Id,
            Name = team.Name
        };
    }
}