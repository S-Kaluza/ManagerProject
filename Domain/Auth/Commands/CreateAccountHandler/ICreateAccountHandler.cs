using Domain.Auth.Commands.CreateAccountHandler.Request;

namespace Domain.Auth.Commands.CreateAccountHandler;

public interface ICreateAccountHandler
{
    Task<CreateAccountRequestResponse> Handle(CreateAccountRequest request);
}