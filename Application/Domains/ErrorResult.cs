namespace Application.Domains;

using System.Net;
using Microsoft.AspNetCore.Mvc;

public class ErrorResult : ObjectResult
{
    public ErrorResult(Error error, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(error)
    {
        StatusCode = (int)statusCode;
    }

    public ErrorResult(int error, string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        object? dataValue = null)
        : this(CreateError(error, message, dataValue), statusCode)
    {
    }

    private static Error CreateError(int code, string message, object? dataValue = null)
    {
        return new Error()
        {
            Code = code,
            Message = message,
            Value = dataValue,
        };
    }
}