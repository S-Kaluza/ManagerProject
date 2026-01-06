using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Companies.Commands.UpadateCompanyHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Companies.Commands.UpadateCompanyHandler;

public class UpdateCompanyHandler : IUpdateCompanyHandler
{
    private readonly ApplicationDbContext _context;
    
    public UpdateCompanyHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<UpdateCompanyResponse> Handle(UpdateCompanyRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());
        
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == currentUser.CompanyId)
            ?? throw new DomainException(ErrorCodeEnum.CompanyNotFound, ErrorCodeEnum.CompanyNotFound.GetDescription());

        if (request.Name != null)
        {
            company.Name = request.Name;
        }
        
        company.Description = request.Description;
        
        await _context.SaveChangesAsync();
        
        return company.Adapt<UpdateCompanyResponse>();
    }
}