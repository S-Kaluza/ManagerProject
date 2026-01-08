using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.DTOs;
using DataAccess;
using Domain.Tasks.Queries.GetTasksByUserIdHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tasks.Queries.GetTasksByUserIdHandler;

public class GetTasksByUserIdHandler : IGetTasksByUserIdHandler
{
    private readonly ApplicationDbContext _context;
    
    public GetTasksByUserIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetTasksByUserIdResponse> Handle(GetTasksByUserIdRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users
                              .Include(x => x.Tasks)
                              .FirstOrDefaultAsync(x =>
                                  x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active)
                          ?? throw new DomainException(ErrorCodeEnum.UserNotFound,
                              ErrorCodeEnum.UserNotFound.GetDescription());
        if (currentUser.Id == request.UserId)
        {
            return new GetTasksByUserIdResponse()
            {
                Tasks = currentUser.Tasks.Adapt<IEnumerable<TaskDTO>>()
            };
        }

        if (currentUser.RolesId != (int)RolesEnum.Admin)
        {
            throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());
        }

        var user = await _context.Users
                       .Include(x => x.Tasks)
                       .FirstOrDefaultAsync(x => x.Id == request.UserId)
                   ?? throw new DomainException(ErrorCodeEnum.UserNotFound,
                       ErrorCodeEnum.UserNotFound.GetDescription());
        return new GetTasksByUserIdResponse()
        {
            Tasks = user.Tasks.Adapt<IEnumerable<TaskDTO>>()
        };
    }
}