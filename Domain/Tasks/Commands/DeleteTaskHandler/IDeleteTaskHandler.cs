using Domain.Tasks.Commands.DeleteTaskHandler.Request;

namespace Domain.Tasks.Commands.DeleteTaskHandler;

public interface IDeleteTaskHandler
{
    Task<DeleteTaskResponse> Handle(DeleteTaskRequest request);
}