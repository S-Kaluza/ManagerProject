namespace GryphonProject.EndpointDefinitions;

public interface IEndpointDefinition
{
    void RegisterEndpoints(WebApplication app);
}