using Application.Models.Entity;

namespace Application.General;

public class Status
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<User> Users { get; set; }
}