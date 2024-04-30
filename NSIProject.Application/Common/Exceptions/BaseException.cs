namespace NSIProject.Application.Common.Exceptions;

public class BaseException : Exception
{
    public object? AdditionalData { get; }

    public BaseException(string message, object? additionalData) : base(message)
    {
        AdditionalData = additionalData;
    }

    public BaseException(string message, Exception innerException, object? additionalData) : base(message,
        innerException)
    {
        AdditionalData = additionalData;
    }
}