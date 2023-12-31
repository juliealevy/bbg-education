using FluentValidation;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Common.Validation;
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, OneOf<TResponse, ValidationFailed>>
    where TRequest : IRequest<TResponse>
{    
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<OneOf<TResponse, ValidationFailed>> Handle(
        TRequest request,
        RequestHandlerDelegate<OneOf<TResponse,ValidationFailed>> next,
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

        return new ValidationFailed(errors);

    }

   
}
