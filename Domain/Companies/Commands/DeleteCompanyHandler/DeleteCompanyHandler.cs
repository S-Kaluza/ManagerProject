using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Companies.Commands.DeleteCompanyHandler.Request;
using Microsoft.EntityFrameworkCore;

namespace Domain.Companies.Commands.DeleteCompanyHandler;

public class DeleteCompanyHandler : IDeleteCompanyHandler
{
    private readonly ApplicationDbContext _context;
    
    public DeleteCompanyHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<DeleteCompanyResponse> Handle(DeleteCompanyRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x =>
                              x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active &&
                              x.RolesId == (int)RolesEnum.Admin)
                          ?? throw new DomainException(ErrorCodeEnum.NotPermission,
                              ErrorCodeEnum.NotPermission.GetDescription());

        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == currentUser.CompanyId)
                      ?? throw new DomainException(ErrorCodeEnum.CompanyNotFound,
                          ErrorCodeEnum.CompanyNotFound.GetDescription());
        
        currentUser.RolesId = (int)RolesEnum.User;
        
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return new DeleteCompanyResponse()
        {
            CompanyId = company.Id
        };
    }
}