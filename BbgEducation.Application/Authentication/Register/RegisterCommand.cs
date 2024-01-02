using BbgEducation.Application.Authentication.Common;
using BbgEducation.Application.Common.Validation;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Authentication.Register;
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<OneOf<AuthenticationResult, ValidationFailed>>;

