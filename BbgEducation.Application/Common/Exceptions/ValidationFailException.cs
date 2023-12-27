using FluentValidation.Results;

namespace BbgEducation.Application.Common.Exceptions;
public class ValidationFailException : Exception, IValidationException
{
    public ValidationFailException(List<ValidationFailure> errors) {
        Errors = errors;
    }
    public List<ValidationFailure> Errors { get; } = new List<ValidationFailure>();




}
