using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Teams.Commands.DeleteTeamByIdHandler.Request;
using Microsoft.EntityFrameworkCore;

namespace Domain.Teams.Commands.DeleteTeamByIdHandler;

public class DeleteTeamHandler : IDeleteTeamHandler
{
    private readonly ApplicationDbContext _context;
    
    public DeleteTeamHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<DeleteTeamResponse> Handle(DeleteTeamRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());
        
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == request.TeamId && x.CompanyId == currentUser.CompanyId)
            ?? throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());
        
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();

        return new DeleteTeamResponse()
        {
            TeamId = team.Id
        };
    }
}