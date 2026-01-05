using System.Security.Claims;
using Application.Abstract;
using Application.Extensions;
using Domain.Auth.Commands.AccountLoginHandler;
using Domain.Auth.Commands.AccountLoginHandler.Request;
using Domain.Auth.Commands.CreateAccountHandler;
using Domain.Auth.Commands.CreateAccountHandler.Request;
using Domain.Auth.Profile.Query.GetUserByIdHandler;
using Domain.Auth.Profile.Query.GetUserByIdHandler.Request;
using Microsoft.AspNetCore.Mvc;

namespace GryphonProject.EndpointDefinitions;

public class AuthEndpoints: EndpointBase<AuthEndpoints>
{
    public override void Register(WebApplication webApplication)
    {
        Initialize(webApplication);
        RouteGroupBuilder group = CreateEndpointGroup();
        group.MapPost("login", LoginUser);
        group.MapPost("register", RegisterUser);
        group.MapGet("profile", GetProfile);
    }

    private async Task<IResult> LoginUser([FromBody] AccountLoginRequest request,
        IAccountLoginHandler accountLoginHandler)
    {
        var result = await accountLoginHandler.Handle(request);
        return Results.Ok(result);
    }

    private async Task<IResult> GetProfile(ClaimsPrincipal claimsPrincipal, IGetUserByIdHandler getUserByIdHandler)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        var getUserByIdRequest = new GetUserByIdRequest()
        {
            CurrentUserId = currentUserId,
            UserId = currentUserId
        };
        var results = await getUserByIdHandler.Handle(getUserByIdRequest);
        return Results.Ok(results);
    }

    private async Task<IResult> RegisterUser([FromBody] CreateAccountRequest request, ICreateAccountHandler createAccountHandler)
    {
        var result = await createAccountHandler.Handle(request);
        return Results.Ok(result);
    }
}