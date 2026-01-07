using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.General;
using DataAccess;
using Domain.Tasks.Commands.CreateTaskHandler.Request;
using Microsoft.EntityFrameworkCore;
using Task = Application.Models.Entity.Task;

namespace Domain.Tasks.Commands.CreateTaskHandler;

public class CreateTaskHandler : ICreateTaskHandler
{
    private readonly ApplicationDbContext _context;

    public CreateTaskHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<CreateTaskResponse> Handle(CreateTaskRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin && x.CompanyId != null)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());
        
        var newTask = new Task()
        {
            Name = request.Name,
            Description = request.Description,
            TaskStatusId = (int)TasksStatusEnum.BACKLOG,
            CompanyId = currentUser.CompanyId?? 0
        };

        if (newTask.CompanyId == 0)
        {
            throw new DomainException(ErrorCodeEnum.CompanyNotFound, ErrorCodeEnum.CompanyNotFound.GetDescription());
        }
        
        await _context.Tasks.AddAsync(newTask);
        await _context.SaveChangesAsync();

        return new CreateTaskResponse()
        {
            TaskId = newTask.Id
        };
    }
}