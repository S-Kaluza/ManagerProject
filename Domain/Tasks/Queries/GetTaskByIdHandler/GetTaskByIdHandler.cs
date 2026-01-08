using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Tasks.Queries.GetTaskByIdHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tasks.Queries.GetTaskByIdHandler;

public class GetTaskByIdHandler : IGetTaskByIdHandler
{
    private readonly ApplicationDbContext _context;
    
    public GetTaskByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetTaskByIdResponse> Handle(GetTaskByIdRequest request)
    {
        request.Validate();

        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == request.TaskId)
                   ?? throw new DomainException(ErrorCodeEnum.TaskNotFound,
                       ErrorCodeEnum.TaskNotFound.GetDescription());

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
            x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
            x.CompanyId == task.CompanyId &&
            (x.RolesId == (int)RolesEnum.Admin || x.TeamId == task.TeamId));
        
        return currentUser == null ? throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription()) : task.Adapt<GetTaskByIdResponse>();
    }
}