using Domain.Tasks.Commands.CreateTaskHandler.Request;

namespace Domain.Tasks.Commands.CreateTaskHandler;

public interface ICreateTaskHandler
{
    Task<CreateTaskResponse> Handle(CreateTaskRequest request);
}