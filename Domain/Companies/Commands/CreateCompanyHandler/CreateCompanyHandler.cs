using Application.Domains;
using Application.Enums;
using Application.Extensions;
using Application.Models.Entity;
using DataAccess;
using Domain.Companies.Commands.CreateCompanyHandler.Request;
using Microsoft.EntityFrameworkCore;

namespace Domain.Companies.Commands.CreateCompanyHandler;

public class CreateCompanyHandler : ICreateCompanyHandler
{
    private readonly ApplicationDbContext _context;
    
    public CreateCompanyHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<CreateCompanyResponse> Handle(CreateCompanyRequest request)
    {
        request.Validate();

        var doesCompanyExist = await _context.Companies.AnyAsync(x => x.Name == request.Name);
        if (doesCompanyExist)
        {
            throw new DomainException(ErrorCodeEnum.CompanyAlreadyExists, ErrorCodeEnum.CompanyAlreadyExists.GetDescription());
        }
        
        var companyInitialUser = await _context.Users.FirstOrDefaultAsync(x => x.CompanyId == null && x.Id == request.CurrentUserId)
            ?? throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());

        Company newCompany = new ()
        {
            Name = request.Name,
            Description = request.Description,
            Users = new List<User>()
            {
                companyInitialUser
            },
        };
        
        companyInitialUser.StatusId = (int)StatusEnum.Active;
        companyInitialUser.RolesId = (int)RolesEnum.Admin;
        _context.Companies.Add(newCompany);
        await _context.SaveChangesAsync();
        
        return new CreateCompanyResponse()
        {
            CompanyId = newCompany.Id
        };
    }
}