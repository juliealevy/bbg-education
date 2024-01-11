using FluentValidation.Results;

namespace BbgEducation.Application.Common.Validation;
public record ValidationFailed(ValidationErrorType ErrorType, IEnumerable<ValidationFailure> Errors) {

    public ValidationFailed(ValidationErrorType errorType, ValidationFailure error): this(errorType, new [] { error }) {

    }

    public ValidationFailed(ValidationErrorType errorType, string propertyName, string message): this(
        errorType, new[] { new ValidationFailure(propertyName, message) }) {

    }

    public static ValidationFailed BadRequest(string propertyName, string message) {
        return new ValidationFailed(ValidationErrorType.BadRequest, propertyName, message);
    }

    public static ValidationFailed BadRequest(IEnumerable<ValidationFailure> errors) {
        return new ValidationFailed(ValidationErrorType.BadRequest, errors);
    }
    public static ValidationFailed Conflict(string propertyName, string message) {
        return new ValidationFailed(ValidationErrorType.Conflict, propertyName, message);
    }
}
