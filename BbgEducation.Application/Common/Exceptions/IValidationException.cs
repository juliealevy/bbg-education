using FluentValidation.Results;

namespace BbgEducation.Application.Common.Exceptions;
public interface IValidationException
{
    public List<ValidationFailure> Errors { get; }
}


