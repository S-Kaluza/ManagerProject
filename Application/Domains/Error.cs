namespace Application.Domains;

public class Error
{
    public int Code { get; set; }
    public string Message { get; set; }
    public object Value { get; set; }
}