using Application.Models.Entity;

namespace Domain.Companies.Query.GetCompanyHandler.Request;

public class GetCompanyResponseAdmin
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Team>? Teams { get; set; }
    public IEnumerable<User>? Users { get; set; }
}