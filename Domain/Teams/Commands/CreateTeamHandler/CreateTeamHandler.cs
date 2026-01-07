using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.Entity;
using DataAccess;
using Domain.Teams.Commands.CreateTeamHandler.Request;
using Microsoft.EntityFrameworkCore;

namespace Domain.Teams.Commands.CreateTeamHandler;

public class CreateTeamHandler : ICreateTeamHandler
{
    private readonly ApplicationDbContext _context;
    
    public CreateTeamHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<CreateTeamResponse> Handle(CreateTeamRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.UserNotFound,
                              ErrorCodeEnum.UserNotFound.GetDescription());
        
        var currentUserCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Id == currentUser.CompanyId)
            ?? throw new DomainException(ErrorCodeEnum.CompanyNotFound, ErrorCodeEnum.CompanyNotFound.GetDescription());

        var newTeam = new Team()
        {
            Name = request.Name,
            CompanyId = currentUserCompany.Id,
        };

        await _context.Teams.AddAsync(newTeam);
        await _context.SaveChangesAsync();

        return new CreateTeamResponse()
        {
            TeamId = newTeam.Id
        };
    }
}