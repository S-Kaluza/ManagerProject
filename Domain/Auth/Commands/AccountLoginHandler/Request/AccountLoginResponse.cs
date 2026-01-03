namespace Domain.Auth.Commands.AccountLoginHandler.Request;

public class AccountLoginResponse
{
    public DateTime Expires { get; set; }
    public string Token { get; set; }
}