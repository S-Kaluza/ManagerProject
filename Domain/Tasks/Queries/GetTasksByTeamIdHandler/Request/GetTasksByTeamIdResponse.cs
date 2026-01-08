using Application.Models.DTOs;

namespace Domain.Tasks.Queries.GetTasksByTeamIdHandler.Request;

public class GetTasksByTeamIdResponse
{
    public IEnumerable<TaskDTO> Tasks { get; set; }
}