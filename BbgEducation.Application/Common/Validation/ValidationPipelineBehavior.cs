using FluentValidation;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Common.Validation;
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse: IOneOf
{    
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {

        if (_validators is null || !_validators.Any())
        {
            return await next(); //calls the handler (next in the pipline)
        }


        var errors = _validators
           .Select(validator => validator.Validate(request))
           .SelectMany(validationResult => validationResult.Errors)
           .Where(validationFailure => validationFailure != null)
           .Distinct().ToList();


        if (!errors.Any())
        {
            return await next();  
        }

        return (dynamic)ValidationFailed.BadRequest(errors);

    }

   
}
