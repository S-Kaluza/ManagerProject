using Domain.Tasks.Queries.GetTasksByCompanyIdHandler.Request;

namespace Domain.Tasks.Queries.GetTasksByCompanyIdHandler;

public interface IGetTasksByCompanyIdHandler
{
    Task<GetTasksByCompanyIdResponse> Handle(GetTasksByCompanyIdRequest request);
}