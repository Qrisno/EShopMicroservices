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
        logger.LogError($"Error Message: {exception.GetType().FullName} Time: {DateTime.Now}");
         string Title = "Internal Server Error"; 
         int StatusCode = StatusCodes.Status500InternalServerError;
        var Detail = exception.Message;
        if (exception.GetType().IsSubclassOf(typeof(NotFoundException))
           )
        {

            Title = "Not Found";
            StatusCode = StatusCodes.Status404NotFound;
        } else if (exception is BadRequestException || exception is ValidationException)
        {

            Title = "Bad Request";
            StatusCode = StatusCodes.Status400BadRequest;
        }else if (exception is InvalidServerException)
        {
            Title = "Server Error";
        }

        var problemDetails = new ProblemDetails
        {
            Title = Title,
            Detail = Detail,
            Status = StatusCode,
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