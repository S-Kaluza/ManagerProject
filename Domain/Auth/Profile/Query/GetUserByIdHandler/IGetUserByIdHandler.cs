using Domain.Auth.Profile.Query.GetUserByIdHandler.Request;

namespace Domain.Auth.Profile.Query.GetUserByIdHandler;

public interface IGetUserByIdHandler
{
    Task<GetUserByIdResponse> Handle(GetUserByIdRequest request);
}