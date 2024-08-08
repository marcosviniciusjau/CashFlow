using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is ErrorOnValidation)
        {
            HandleException(context);
        } else
        {
            ThrowUnknownErrors(context);
        }
    }

    private void HandleException(ExceptionContext context)
    {
        if(context.Exception is ErrorOnValidation)
        {
            var ex = (ErrorOnValidation)context.Exception;
            var errorResponse = new ResponseError(ex.Errors);
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Result = new ObjectResult(errorResponse);
        }
    }

    private void ThrowUnknownErrors(ExceptionContext context)
    {
        var errorResponse = new ResponseError("unknow error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        context.Result = new ObjectResult(errorResponse);

    }
}
