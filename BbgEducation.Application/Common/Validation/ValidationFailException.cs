using BbgEducation.Application.Common.Interfaces.Exceptions;
using FluentValidation.Results;

namespace BbgEducation.Application.Common.Validation;
public class ValidationFailException : Exception, IValidationException
{
    public ValidationFailException(List<ValidationFailure> errors)
    {
        Errors = errors;
    }
    public List<ValidationFailure> Errors { get; } = new List<ValidationFailure>();




}
