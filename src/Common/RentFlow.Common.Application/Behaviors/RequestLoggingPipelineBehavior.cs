using RentFlow.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace RentFlow.Common.Application.Behaviors;

internal sealed partial class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string moduleName = GetModuleName(typeof(TRequest).FullName!);
        string requestName = typeof(TRequest).Name;

        using (LogContext.PushProperty("Module", moduleName))
        {
            LogProcessingRequest(logger, requestName);

            TResponse result = await next(cancellationToken);

            if (result.IsSuccess)
            {
                LogCompletedRequest(logger, requestName);
                
                return result;
            }
            
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                LogCompletedRequestWithError(logger, requestName);
            }

            return result;
        }
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "Processing request {RequestName}")]
    private static partial void LogProcessingRequest(ILogger logger, string requestName);

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Information,
        Message = "Completed request {RequestName}")]
    private static partial void LogCompletedRequest(ILogger logger, string requestName);

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Error,
        Message = "Completed request {RequestName} with error")]
    private static partial void LogCompletedRequestWithError(ILogger logger, string requestName);

    private static string GetModuleName(string requestName) => requestName.Split('.')[2];
}
