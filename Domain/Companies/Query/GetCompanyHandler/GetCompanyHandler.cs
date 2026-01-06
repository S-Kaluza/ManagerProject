using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Companies.Query.GetCompanyHandler.Request;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Domain.Companies.Query.GetCompanyHandler;

public class GetCompanyHandler : IGetCompanyHandler
{
    private readonly ApplicationDbContext _context;
    
    public GetCompanyHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetCompanyResponseAdmin> Handle(GetCompanyRequest request)
    {
        request.Validate();

        var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.CurrentUserId && x.StatusId == (int)StatusEnum.Active)
                          ?? throw new DomainException(ErrorCodeEnum.UserNotFound,
                              ErrorCodeEnum.UserNotFound.GetDescription());

        if (currentUser.RolesId == (int)RolesEnum.User)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == currentUser.CompanyId)
                ?? throw new DomainException(ErrorCodeEnum.CompanyNotFound, ErrorCodeEnum.CompanyNotFound.GetDescription());
            var response = company.Adapt<GetCompanyResponseAdmin>();
            response.Teams = null;
            response.Users = null;
            return response;
        }
        else
        {
            var company = await _context.Companies
                .Include(x => x.Users)
                .Include(x => x.Teams)
                .FirstOrDefaultAsync(x => x.Id == currentUser.CompanyId)
                ?? throw new DomainException(ErrorCodeEnum.CompanyNotFound, ErrorCodeEnum.CompanyNotFound.GetDescription());
            return company.Adapt<GetCompanyResponseAdmin>();
        }
    }
}