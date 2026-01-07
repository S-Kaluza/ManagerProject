using Domain.Tasks.Commands.UpdateTaskHandler.Request;

namespace Domain.Tasks.Commands.UpdateTaskHandler;

public interface IUpdateTaskHandler
{
    Task<UpdateTaskResponse> Handle(UpdateTaskRequest request);
}