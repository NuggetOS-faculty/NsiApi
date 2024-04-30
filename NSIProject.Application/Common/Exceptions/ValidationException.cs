namespace NSIProject.Application.Common.Exceptions;

public class ValidationException : BaseException
{
    public ValidationException(IDictionary<string, string[]> errors, object? additionalData) : base(
        "One or more validations failed", errors)

    {
    }
}