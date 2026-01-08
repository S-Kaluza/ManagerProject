using System.Security.Claims;
using Application.Abstract;
using Application.Extensions;
using Domain.Teams.Commands.CreateTeamHandler;
using Domain.Teams.Commands.CreateTeamHandler.Request;
using Domain.Teams.Commands.DeleteTeamByIdHandler;
using Domain.Teams.Commands.DeleteTeamByIdHandler.Request;
using Domain.Teams.Commands.InviteUserToTeamHandler;
using Domain.Teams.Commands.InviteUserToTeamHandler.Request;
using Domain.Teams.Commands.UpdateTeamHandler;
using Domain.Teams.Commands.UpdateTeamHandler.Request;
using Domain.Teams.Queries.GetTeamByIdHandler;
using Domain.Teams.Queries.GetTeamByIdHandler.Request;
using Domain.Teams.Queries.GetTeamsByCompanyIdHandler;
using Domain.Teams.Queries.GetTeamsByCompanyIdHandler.Request;
using Microsoft.AspNetCore.Mvc;

namespace GryphonProject.EndpointDefinitions;

public class TeamEndpoints: EndpointBase<TeamEndpoints>
{
    public override void Register(WebApplication webApplication)
    {
        Initialize(webApplication);
        RouteGroupBuilder group = CreateEndpointGroup();
        group.MapPost("create", CreateTeam);
        group.MapGet("get", GetTeamById);
        group.MapPost("get-by-company", GetTeamByCompanyId);
        group.MapPost("invite", InviteUser);
        group.MapPut("update", UpdateTeam);
        group.MapDelete("delete", DeleteTeam);
    }
    
    private async Task<IResult> CreateTeam([FromBody] CreateTeamRequest request,
        ICreateTeamHandler createTaskHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUserId;
        var result = await createTaskHandler.Handle(request);
        return Results.Ok(result);
    }
    
    private async Task<IResult> GetTeamById([FromQuery] int teamId, IGetTeamByIdHandler getTaskByIdHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        var request = new GetTeamByIdRequest()
        {
            CurrentUserId = currentUserId,
            TeamId = teamId
        };
        var result = await getTaskByIdHandler.Handle(request);
        return Results.Ok(result);
    }
    
    private async Task<IResult> GetTeamByCompanyId([FromBody] GetTeamsByCompanyIdRequest request, IGetTeamsByCompanyIdHandler getTeamsByCompanyIdHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUserId;
        var result = await getTeamsByCompanyIdHandler.Handle(request);
        return Results.Ok(result);
    }
    
    private async Task<IResult> UpdateTeam([FromBody] UpdateTeamRequest updateTeamRequest,
        IUpdateTeamHandler updateTeamHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = claimsPrincipal.GetUserIdFromClaims();
        updateTeamRequest.CurrentUserId = currentUser;
        var result = await updateTeamHandler.Handle(updateTeamRequest);
        return Results.Ok(result);
    }
    
    private async Task<IResult> DeleteTeam([FromBody]DeleteTeamRequest request, IDeleteTeamHandler deleteTeamHandler,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUser;
        var result = await deleteTeamHandler.Handle(request);
        return Results.Ok(result);
    }

    private async Task<IResult> InviteUser([FromBody] InviteUserToTeamRequest request,
        IInviteUserToTeamHandler inviteUserToTeamHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUser;
        var result = await inviteUserToTeamHandler.Handle(request);
        return Results.Ok(result);
    }
}