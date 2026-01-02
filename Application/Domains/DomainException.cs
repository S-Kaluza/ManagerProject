using Application.Enums;

namespace Application.Domains;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(ErrorCodeEnum code, string message) : base(message)
    {
        Code = (int)code;
    }

    public int Code { get; }
}