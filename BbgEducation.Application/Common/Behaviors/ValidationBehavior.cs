using BbgEducation.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace BbgEducation.Application.Common.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    //private readonly IValidator<TRequest>? _validator;
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
        _validators = validators;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken) {

        if (!_validators.Any()) {
            return await next();
        }


         var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .Distinct().ToList();


        if (!errors.Any()) {
            return await next();  //calls the handler
        }

        throw new ValidationFailException(errors);
    }
}
