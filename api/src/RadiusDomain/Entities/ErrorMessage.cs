namespace RadiusDomain.Entities;

public class ErrorMessage
{
    public ErrorMessage(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; init; }

    public string Message { get; init; }
}