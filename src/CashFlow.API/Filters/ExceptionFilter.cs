using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
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
        else
        {
            var errorResponse = new ResponseError(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Result = new ObjectResult(errorResponse);
        }
    }

    private void ThrowUnknownErrors(ExceptionContext context)
    {
        var errorResponse = new ResponseError(ResourceErrorMessages.Unknow_Error);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        context.Result = new ObjectResult(errorResponse);

    }
}


