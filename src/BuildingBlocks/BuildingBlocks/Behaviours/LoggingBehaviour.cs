using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviours;

public class LoggingBehaviour<TRequest,TResponse>(ILogger<LoggingBehaviour<TRequest,TResponse>> logger):IPipelineBehavior<TRequest,TResponse>
where TRequest: notnull, IRequest<TResponse> where TResponse:notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
       logger.LogInformation($"{typeof(TRequest).Name}- Requested {typeof(TResponse).Name}-Response. Request Data  {request}");
       var timer = new Stopwatch();
       timer.Start();
       var response = await next();
       timer.Stop();
       logger.LogInformation($"{typeof(TRequest).Name}- Requested {typeof(TResponse).Name}-Response. Request Data  {request} Time {timer.ElapsedMilliseconds} ms");
       return response;
    }
}