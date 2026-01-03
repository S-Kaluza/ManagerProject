using Microsoft.AspNetCore.Builder;

namespace Application.Abstract;

public interface IEndpoints
{
    public abstract void Register(WebApplication webApplication);
}