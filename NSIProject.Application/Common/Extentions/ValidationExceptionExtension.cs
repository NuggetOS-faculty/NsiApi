using FluentValidation.Results;

namespace NSIProject.Application.Common.Extentions;

public static class ValidationExceptionExtension
{
    public static IDictionary<string, string[]> ToGroup(this List<ValidationFailure> failures)
    {
        var groupedFailures = failures.GroupBy(f => f.PropertyName, f => f.ErrorMessage);
        return groupedFailures.ToDictionary(g => g.Key, g => g.ToArray());
    }
}