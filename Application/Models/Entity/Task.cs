using Application.General;

namespace Application.Models.Entity;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public TasksStatus TasksStatus { get; set; }
    public int TaskStatusId { get; set; }
    public IEnumerable<User>? Users { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public Team Team { get; set; }
    public int TeamId { get; set; }
}