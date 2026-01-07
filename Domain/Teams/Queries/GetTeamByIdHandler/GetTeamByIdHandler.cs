using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Teams.Queries.GetTeamByIdHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Teams.Queries.GetTeamByIdHandler;

public class GetTeamByIdHandler : IGetTeamByIdHandler
{
    private readonly ApplicationDbContext _context;
    
    public GetTeamByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<GetTeamByIdResponse> Handle(GetTeamByIdRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              (x.RolesId == (int)RolesEnum.Admin || x.TeamId == request.TeamId))
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());

        var team = await _context.Teams.FirstOrDefaultAsync(x =>
                       x.Id == request.TeamId && x.CompanyId == currentUser.CompanyId)
                   ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                       ErrorCodeEnum.NotPermission.GetDescription());

        return team.Adapt<GetTeamByIdResponse>();
    }
}