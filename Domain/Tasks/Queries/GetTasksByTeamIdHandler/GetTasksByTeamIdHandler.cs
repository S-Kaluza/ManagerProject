using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.DTOs;
using DataAccess;
using Domain.Tasks.Queries.GetTasksByTeamIdHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tasks.Queries.GetTasksByTeamIdHandler;

public class GetTasksByTeamIdHandler : IGetTasksByTeamIdHandler
{
    private readonly ApplicationDbContext _context;
    
    public GetTasksByTeamIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetTasksByTeamIdResponse> Handle(GetTasksByTeamIdRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
            x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active)
            ?? throw new DomainException(ErrorCodeEnum.UserNotFound, ErrorCodeEnum.UserNotFound.GetDescription());

        if ((currentUser.TeamId != request.TeamId) && currentUser.RolesId != (int)RolesEnum.Admin)
        {
            throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());
        }
        
        var tasks = await _context.Tasks.Where(x => x.TeamId == request.TeamId).ToListAsync();

        return new GetTasksByTeamIdResponse()
        {
            Tasks = tasks.Adapt<IEnumerable<TaskDTO>>()
        };
    }
}