using Application.Models.DTOs;

namespace Domain.Tasks.Queries.GetTasksByCompanyIdHandler.Request;

public class GetTasksByCompanyIdResponse
{
    public IEnumerable<TaskDTO> Tasks { get; set; }
}