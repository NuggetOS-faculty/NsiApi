using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NSIProject.Application.Common.Exceptions;

namespace NSIProject.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandler;

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    public ApiExceptionFilter()
    {
        _exceptionHandler = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ArgumentException), HandleArgumentException },
            { typeof(ArgumentNullException), HandleArgumentNullException },
            { typeof(InvalidOperationException), HandleInvalidOperationException },
            { typeof(ValidationException), HandleValidationException },
            { typeof(FluentValidation.ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException }
        };
    }


    void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;
        context.Result = new JsonResult(new
        {
            error = exception.Message
        })
        {
            StatusCode = 404
        };
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandler.TryGetValue(type,
                out var handler))
        {
            handler.Invoke(context);
            return;
        }

        HandleUnknownException(context);
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