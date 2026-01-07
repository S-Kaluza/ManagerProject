using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Tasks.Commands.DeleteTaskHandler.Request;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tasks.Commands.DeleteTaskHandler;

public class DeleteTaskHandler : IDeleteTaskHandler
{
    private readonly ApplicationDbContext _context;

    public DeleteTaskHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<DeleteTaskResponse> Handle(DeleteTaskRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.currentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());

        var task = await _context.Tasks.FirstOrDefaultAsync(x =>
                       x.Id == request.taskId && x.CompanyId == currentUser.CompanyId)
                   ?? throw new DomainException(ErrorCodeEnum.TaskNotFound,
                       ErrorCodeEnum.TaskNotFound.GetDescription());

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return new DeleteTaskResponse()
        {
            TaskId = task.Id
        };
    }
}