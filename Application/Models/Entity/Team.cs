namespace Application.Models.Entity;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Company Company { get; set; }
    public int CompanyId { get; set; }
    public IEnumerable<User> Users { get; set; }
}