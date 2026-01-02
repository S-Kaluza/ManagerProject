namespace GryphonProject.EndpointDefinitions;

public class UserEndpointsDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var users = app.MapGroup("/api/users");
        users.MapGet("/{id}", GetUser)
            .WithName("getUserById");
    }

    public async Task<IResult> GetUser(int id)
    {
        return Results.Ok(id);
    }
}