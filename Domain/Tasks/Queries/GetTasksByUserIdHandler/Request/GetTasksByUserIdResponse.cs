using Application.Models.DTOs;

namespace Domain.Tasks.Queries.GetTasksByUserIdHandler.Request;

public class GetTasksByUserIdResponse
{
    public IEnumerable<TaskDTO> Tasks { get; set; }
}