using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Abstract;

public abstract class EndpointBase<T> : IEndpoints
{
    protected ILogger<T>? Logger { get; set; }
        
    protected string? BaseUrl { get; set; }
        
    protected WebApplication? _webApplication { get; set; }

    protected void Initialize(WebApplication webApplication)
    {
        _webApplication = webApplication;
        Logger = webApplication.Services.GetRequiredService<ILogger<T>>();
        string endpointName = typeof(T).Name;
        BaseUrl = endpointName![..^"Endpoints".Length].ToLower();
    }

    protected RouteGroupBuilder CreateEndpointGroup()
    {
        return _webApplication.MapGroup("/api/" + CreateUrl());
    }

    protected string CreateUrl(string? url = null)
    {
        if (url == null)
        {
            return BaseUrl;
        }
        return BaseUrl + "/" + url;
    }

    public abstract void Register(WebApplication webApplication);
}