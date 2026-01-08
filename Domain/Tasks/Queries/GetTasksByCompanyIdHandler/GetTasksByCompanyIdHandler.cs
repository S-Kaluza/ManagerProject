using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.DTOs;
using DataAccess;
using Domain.Tasks.Queries.GetTasksByCompanyIdHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tasks.Queries.GetTasksByCompanyIdHandler;

public class GetTasksByCompanyIdHandler : IGetTasksByCompanyIdHandler
{
    private readonly ApplicationDbContext _context;
    
    public GetTasksByCompanyIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetTasksByCompanyIdResponse> Handle(GetTasksByCompanyIdRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());

        if (currentUser.CompanyId != request.CompanyId)
            throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());
        {
            var tasks = await _context.Tasks.Where(x => x.CompanyId == currentUser.CompanyId).ToListAsync();
            return new GetTasksByCompanyIdResponse()
            {
                Tasks = tasks.Adapt<IEnumerable<TaskDTO>>()
            };
        }

    }
}