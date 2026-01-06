using System.Security.Claims;
using Application.Abstract;
using Application.Extensions;
using Domain.Companies.Commands.CreateCompanyHandler;
using Domain.Companies.Commands.CreateCompanyHandler.Request;
using Domain.Companies.Commands.DeleteCompanyHandler;
using Domain.Companies.Commands.DeleteCompanyHandler.Request;
using Domain.Companies.Commands.UpadateCompanyHandler;
using Domain.Companies.Commands.UpadateCompanyHandler.Request;
using Domain.Companies.Query.GetCompanyHandler;
using Domain.Companies.Query.GetCompanyHandler.Request;
using Microsoft.AspNetCore.Mvc;

namespace GryphonProject.EndpointDefinitions;

public class CompanyEndpoints: EndpointBase<CompanyEndpoints>
{
    public override void Register(WebApplication webApplication)
    {
        Initialize(webApplication);
        RouteGroupBuilder group = CreateEndpointGroup();
        group.MapPost("create", CreateCompany);
        group.MapGet("get", GetCompany);
        group.MapPut("update", UpdateCompany);
        group.MapDelete("delete", DeleteCompany);
    }

    private async Task<IResult> CreateCompany([FromBody] CreateCompanyRequest request,
        ICreateCompanyHandler createCompanyHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        request.CurrentUserId = currentUserId;
        var result = await createCompanyHandler.Handle(request);
        return Results.Ok(result);
    }

    private async Task<IResult> GetCompany(IGetCompanyHandler getUserByIdHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserId = claimsPrincipal.GetUserIdFromClaims();
        var request = new GetCompanyRequest()
        {
            CurrentUserId = currentUserId
        };
        var result = await getUserByIdHandler.Handle(request);
        return Results.Ok(result);
    }

    private async Task<IResult> UpdateCompany([FromBody] UpdateCompanyRequest updateCompanyRequest,
        IUpdateCompanyHandler updateCompanyHandler, ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = claimsPrincipal.GetUserIdFromClaims();
        updateCompanyRequest.CurrentUserId = currentUser;
        var result = await updateCompanyHandler.Handle(updateCompanyRequest);
        return Results.Ok(result);
    }

    private async Task<IResult> DeleteCompany(IDeleteCompanyHandler deleteCompanyHandler,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = claimsPrincipal.GetUserIdFromClaims();
        var request = new DeleteCompanyRequest()
        {
            CurrentUserId = currentUser
        };
        var result = await deleteCompanyHandler.Handle(request);
        return Results.Ok(result);
    }
}