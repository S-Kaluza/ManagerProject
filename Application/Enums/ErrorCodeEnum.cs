using System.ComponentModel;

namespace Application.Enums;

public enum ErrorCodeEnum
{
    [Description("User already exists")] UserAlreadyExists = -1,

    [Description("User not found")] UserNotFound = -2,

    [Description("Email not confirmed")] UserEmailIsNotConfirmed = -3,

    [Description("General error - contact with administrator and try again later")]
    GeneralError = -500
}