using Task = Application.Models.Entity.Task;

namespace Application.General;

public class TasksStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Task> Tasks { get; set; }
}