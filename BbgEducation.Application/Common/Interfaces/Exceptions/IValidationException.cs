using FluentValidation.Results;

namespace BbgEducation.Application.Common.Interfaces.Exceptions;
public interface IValidationException
{
    public List<ValidationFailure> Errors { get; }
}


