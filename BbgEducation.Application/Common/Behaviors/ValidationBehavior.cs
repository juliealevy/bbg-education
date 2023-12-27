﻿using BbgEducation.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace BbgEducation.Application.Common.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator) {
        _validator = validator;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken) {

        if (_validator is null) {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid) {
            return await next();  //calls the handler
        }

        throw new ValidationFailException(validationResult.Errors);
    }
}
