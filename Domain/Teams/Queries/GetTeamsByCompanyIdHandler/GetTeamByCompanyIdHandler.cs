using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.DTOs;
using DataAccess;
using Domain.Teams.Queries.GetTeamsByCompanyIdHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Teams.Queries.GetTeamsByCompanyIdHandler;

public class GetTeamsByCompanyIdHandler : IGetTeamsByCompanyIdHandler
{
    
    private readonly ApplicationDbContext _context;
    
    public GetTeamsByCompanyIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<GetTeamsByCompanyIdResponse> Handle(GetTeamsByCompanyIdRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());

        if (request.CompanyId != currentUser.CompanyId)
        {
            throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());
        }
        
        var teams = await _context.Teams.Where(x => x.CompanyId == currentUser.CompanyId).ToListAsync();

        return new GetTeamsByCompanyIdResponse()
        {
            Teams = teams.Adapt<IEnumerable<TeamDTO>>()
        };
    }
}