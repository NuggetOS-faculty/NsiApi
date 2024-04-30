using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NSIProject.Application.Common.Exceptions;

namespace NSIProject.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandler;

    public ApiExceptionFilter()
    {
        _exceptionHandler = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ArgumentException), HandleArgumentException },
            { typeof(ArgumentNullException), HandleArgumentNullException },
            { typeof(InvalidOperationException), HandleInvalidOperationException },
            { typeof(ValidationException), HandleValidationException },
            { typeof(FluentValidation.ValidationException), HandleValidationException }
        };
    }

    void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as BaseException;
        context.Result = new JsonResult(new
        {
            error = exception.Message
        })
        {
            StatusCode = 400
        };
    }

    public override void OnException(ExceptionContext context)
    {
        context.Result = new JsonResult(new
        {
            error = context.Exception.Message
        })
        {
            StatusCode = 500
        };
    }

    void HandleArgumentException(ExceptionContext context)
    {
        context.Result = new JsonResult(new
        {
            error = context.Exception.Message
        })
        {
            StatusCode = 400
        };
    }

    void HandleArgumentNullException(ExceptionContext context)
    {
        context.Result = new JsonResult(new
        {
            error = context.Exception.Message
        })
        {
            StatusCode = 400
        };
    }

    void HandleInvalidOperationException(ExceptionContext context)
    {
        context.Result = new JsonResult(new
        {
            error = context.Exception.Message
        })
        {
            StatusCode = 400
        };
    }
}