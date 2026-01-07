using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Tasks.Commands.UpdateTaskHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tasks.Commands.UpdateTaskHandler;

public class UpdateTaskHandler : IUpdateTaskHandler
{
    private readonly ApplicationDbContext _context;
    
    public UpdateTaskHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());

        var task = await _context.Tasks.FirstOrDefaultAsync(x =>
                       x.Id == request.TaskId && x.CompanyId == currentUser.CompanyId)
                   ?? throw new DomainException(ErrorCodeEnum.TaskNotFound,
                       ErrorCodeEnum.TaskNotFound.GetDescription());

        if (request.Name != null)
        {
            task.Name = request.Name;
        }

        if (request.Description != null)
        {
            task.Description = request.Description;
        }

        if (request.TasksStatusId != null)
        {
            task.TaskStatusId = request.TasksStatusId ?? throw new DomainException(ErrorCodeEnum.GeneralError, ErrorCodeEnum.GeneralError.GetDescription());
        }

        await _context.SaveChangesAsync();
        return task.Adapt<UpdateTaskResponse>();
    }
}