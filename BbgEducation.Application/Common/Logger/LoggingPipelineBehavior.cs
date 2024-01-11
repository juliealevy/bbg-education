using BbgEducation.Application.Common.Validation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Logger;
public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse: IOneOf
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken) {

        _logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", 
            typeof(TRequest).Name, DateTime.UtcNow);

        var result = await next();

        if (result is IOneOf oneOfResult) {
            if (oneOfResult.Value is ValidationFailed fail) {
                _logger.LogError("Request failure {@RequestName}, {Errors}, {@DateTimeUtc} ",
                     typeof(TRequest).Name, fail.Errors, DateTime.UtcNow);
            }
        }
        

        _logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name, DateTime.UtcNow);

        return result;
    }
}
