using System.Security.Claims;
using Application.Abstract;
using Application.Extensions;
using Domain.Tasks.Commands.CreateTaskHandler;
using Domain.Tasks.Commands.CreateTaskHandler.Request;
using Domain.Tasks.Commands.DeleteTaskHandler;
using Domain.Tasks.Commands.DeleteTaskHandler.Request;
using Domain.Tasks.Commands.UpdateTaskHandler;
using Domain.Tasks.Commands.UpdateTaskHandler.Request;
using Domain.Tasks.Queries.GetTaskByIdHandler;
using Domain.Tasks.Queries.GetTaskByIdHandler.Request;
using Domain.Tasks.Queries.GetTasksByCompanyIdHandler;
using Domain.Tasks.Queries.GetTasksByCompanyIdHandler.Request;
using Domain.Tasks.Queries.GetTasksByTeamIdHandler;
using Domain.Tasks.Queries.GetTasksByTeamIdHandler.Request;
using Domain.Tasks.Queries.GetTasksByUserIdHandler;
using Domain.Tasks.Queries.GetTasksByUserIdHandler.Request;
using Microsoft.AspNetCore.Mvc;

namespace GryphonProject.EndpointDefinitions;

public class TaskEndpoints: EndpointBase<TaskEndpoints>
{
    public override void Register(WebApplication webApplication)
    {
        Initialize(webApplication);
        RouteGroupBuilder group = CreateEndpointGroup();
        group.MapPost("create", CreateTask);
        group.MapGet("get", GetTaskById);
        group.MapPost("get-by-company", GetTaskByCompanyId);
        group.MapPost("get-by-team", GetTaskByTeamId);
        group.MapPost("get-by-user", GetTaskByUserId);
        group.MapPut("update", UpdateTask);
        group.MapDelete("delete", DeleteTask);
    }

    private async Task<IResult> CreateTask([FromBody] CreateTaskRequest request,
        ICreateTaskHandler createTaskHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUserId;
        var result = await createTaskHandler.Handle(request);
        return Results.Ok(result);
    }
    
    private async Task<IResult> GetTaskById([FromQuery] int taskId, IGetTaskByIdHandler getTaskByIdHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        var request = new GetTaskByIdRequest()
        {
            CurrentUserId = currentUserId,
            TaskId = taskId
        };
        var result = await getTaskByIdHandler.Handle(request);
        return Results.Ok(result);
    }
    
    private async Task<IResult> GetTaskByCompanyId([FromBody] GetTasksByCompanyIdRequest request, IGetTasksByCompanyIdHandler getTasksByCompanyIdHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUserId;
        var result = await getTasksByCompanyIdHandler.Handle(request);
        return Results.Ok(result);
    }
    
    private async Task<IResult> GetTaskByTeamId([FromBody] GetTasksByTeamIdRequest request, IGetTasksByTeamIdHandler getTasksByTeamIdHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUserId;
        var result = await getTasksByTeamIdHandler.Handle(request);
        return Results.Ok(result);
    }
    
    private async Task<IResult> GetTaskByUserId([FromBody] GetTasksByUserIdRequest request, IGetTasksByUserIdHandler getTasksByUserIdHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUserId;
        var result = await getTasksByUserIdHandler.Handle(request);
        return Results.Ok(result);
    }
    
    
    private async Task<IResult> UpdateTask([FromBody] UpdateTaskRequest updateTaskRequest,
        IUpdateTaskHandler updateTaskHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = claimsPrincipal.GetUserIdFromClaims();
        updateTaskRequest.CurrentUserId = currentUser;
        var result = await updateTaskHandler.Handle(updateTaskRequest);
        return Results.Ok(result);
    }
    
    private async Task<IResult> DeleteTask([FromBody] DeleteTaskRequest request, IDeleteTaskHandler deleteTaskHandler,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUser;
        var result = await deleteTaskHandler.Handle(request);
        return Results.Ok(result);
    }
}