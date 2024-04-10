using BbgEducation.Application.Courses.Create;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace BbgEducation.Application.UnitTests.Courses.Create;
public class CourseCreateCommandValidator : AbstractValidator<CourseCreateCommand>
{
    public CourseCreateCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(5);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}