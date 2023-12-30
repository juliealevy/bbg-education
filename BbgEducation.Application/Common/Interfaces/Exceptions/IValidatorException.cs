using FluentValidation.Results;

namespace BbgEducation.Application.Common.Interfaces.Exceptions;
public interface IValidatorException
{
    public List<ValidationFailure> Errors { get; }
}


