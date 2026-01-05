using Application.Domains;
using Application.Enums;
using Application.Extensions;
using DataAccess;
using Domain.Auth.Profile.Query.GetUserByIdHandler.Request;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Domain.Auth.Profile.Query.GetUserByIdHandler;

public class GetUserByIdHandler : IGetUserByIdHandler
{
    private readonly ApplicationDbContext _context;
    
    public GetUserByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request)
    {
        request.Validate();

        if (request.CurrentUserId != request.UserId)
        {
            throw new DomainException(ErrorCodeEnum.NotPermission, ErrorCodeEnum.NotPermission.GetDescription());
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
        
        return user.Adapt<GetUserByIdResponse>()?? new GetUserByIdResponse();
    }
}