using System.ComponentModel;

namespace Application.Enums;

public enum ErrorCodeEnum
{
    [Description("User already exists")] UserAlreadyExists = -1,

    [Description("User not found")] UserNotFound = -2,

    [Description("Email not confirmed")] UserEmailIsNotConfirmed = -3,
    
    [Description("Email already confirmed")] UserEmailAlreadyConfirmed = -4,
    
    [Description("Invalid credentials")] InvalidCredentials = -5,
    
    [Description("Token not generated")] TokenNotGenerated = -6,
    
    [Description("Email not sent")] EmailNotSent = -7,
    
    [Description("Not permission")] NotPermission = -8,
    
    [Description("Company already exists")] CompanyAlreadyExists = -11,
    
    [Description("Company not found")] CompanyNotFound = -12,

    [Description("General error - contact with administrator and try again later")]
    GeneralError = -500
}