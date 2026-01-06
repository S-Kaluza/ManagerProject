namespace Application.Models.Entity;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Team> Teams { get; set; }
    public IEnumerable<User> Users { get; set; }
}