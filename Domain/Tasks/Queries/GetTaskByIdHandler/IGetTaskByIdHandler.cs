using Domain.Tasks.Queries.GetTaskByIdHandler.Request;

namespace Domain.Tasks.Queries.GetTaskByIdHandler;

public interface IGetTaskByIdHandler
{
    Task<GetTaskByIdResponse> Handle(GetTaskByIdRequest request);
}