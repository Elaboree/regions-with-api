namespace Cleverbit.CodingCase.Application.Models;

public class ExceptionModel
{
    public string Message { get; set; }
    public string MethodName { get; set; }
    public string InnerException { get; set; }
    public string StackTrace { get; set; }

    public ExceptionModel(string? methodName, string message, string? innerException, string? stackTrace)
    {
        MethodName = methodName ?? string.Empty;
        Message = message ?? string.Empty;
        InnerException = innerException ?? string.Empty;
        StackTrace = stackTrace ?? string.Empty;
    }
}