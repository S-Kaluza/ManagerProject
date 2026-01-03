using Domain.Auth.Commands.SendConfirmationEmail.Request;

namespace Domain.Auth.Commands.SendConfirmationEmail;

public interface ISendConfirmationEmailRequestHandler
{
    Task Handle(SendConfirmationEmailRequest request);
}