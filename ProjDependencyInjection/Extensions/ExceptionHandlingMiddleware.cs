using Application.Helpers.Sql;
using System.Text;
using Application.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace ProjDependencyInjection.Extensions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IActionResultExecutor<ObjectResult> _executor;
    private readonly ILogger _logger;
    
    public ExceptionHandlingMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _executor = executor;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            httpContext.Request.EnableBuffering();
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            ExceptionContext errorContext = ErrorHelper.GetExceptionContext(ex);
            errorContext.RequestContext = await GetRequestContext(httpContext);

            RouteData routeData = httpContext.GetRouteData() ?? new RouteData();
            ActionContext actionContext = new(httpContext, routeData, new ActionDescriptor());
            await _executor.ExecuteAsync(actionContext, errorContext.ObjectResult!);
        }
    }
    
    private static async Task<RequestContext> GetRequestContext(HttpContext context)
    {
        string requestParams = context.Request.Method.ToLower() == HttpMethod.Post.ToString().ToLower()
                               || context.Request.Method.ToLower() == HttpMethod.Put.ToString().ToLower()
            ? await GetRequestBody(context)
            : context.Request.QueryString.ToString();
        string userIp = context.Connection.RemoteIpAddress!.ToString();
        return new RequestContext
        {
            IP = userIp,
            RequestParams = requestParams
        };
    }

    private static async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.Body.Position = 0;
        using StreamReader reader = new(context.Request.Body, Encoding.UTF8, true, 1024, true);
        string body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;
        return body;
    }
}