using Domain.Tasks.Queries.GetTasksByUserIdHandler.Request;

namespace Domain.Tasks.Queries.GetTasksByUserIdHandler;

public interface IGetTasksByUserIdHandler
{
    Task<GetTasksByUserIdResponse> Handle(GetTasksByUserIdRequest request);
}