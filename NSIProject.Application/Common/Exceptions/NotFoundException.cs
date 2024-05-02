namespace NSIProject.Application.Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message, object? additionalData = null) : base(message,
        additionalData)
    {
    }
}