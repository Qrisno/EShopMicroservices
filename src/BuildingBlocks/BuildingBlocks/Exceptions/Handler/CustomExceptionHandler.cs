using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace BuildingBlocks.Exceptions.Handler;


public class CustomExceptionHandler(ILogger<CustomExceptionHandler
>    logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError($"Error Message: {exception.Message} Time: {DateTime.Now}");
        (string Detial, string Title, int StatusCode) details = exception switch
        {
            NotFoundException => (exception.Message, "Not Found", StatusCodes.Status404NotFound),
            BadRequestException => (exception.Message, "Bad Request", StatusCodes.Status400BadRequest),
            ValidationException => (exception.Message, "Bad Request", StatusCodes.Status400BadRequest),
            InvalidServerException => (exception.Message, "Server Error", StatusCodes.Status500InternalServerError),
            _ => (exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detial,
            Status = details.StatusCode,
            Instance = context.Request.Path,
        };
        
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        if (exception is ValidationException validationException)
        {
          
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors.Select(e => e.ErrorMessage));
        }
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}