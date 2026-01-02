using Application.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Application.Domains;

public class ExceptionContext
{
    public ObjectResult? ObjectResult { get; set; }
    public RequestContext? RequestContext { get; set; }
    public ErrorLevelEnum ErrorLevel { get; set; }
    public int ErrorCode { get; set; }
    public Exception? Exception { get; set; }
}