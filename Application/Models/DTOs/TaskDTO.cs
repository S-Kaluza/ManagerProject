namespace Application.Models.DTOs;

public class TaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeamId { get; set; }
    public IEnumerable<UserDTO> Users { get; set; }
}