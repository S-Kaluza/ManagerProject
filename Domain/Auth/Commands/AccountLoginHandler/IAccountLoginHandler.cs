using Domain.Auth.Commands.AccountLoginHandler.Request;

namespace Domain.Auth.Commands.AccountLoginHandler;

public interface IAccountLoginHandler
{
    Task<AccountLoginResponse> Handle(AccountLoginRequest request);
}