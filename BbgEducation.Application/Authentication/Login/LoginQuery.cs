using BbgEducation.Application.Authentication.Common;
using BbgEducation.Application.Common.Validation;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Authentication.Login;
public record LoginQuery(string Email, string Password) : IRequest<OneOf<AuthenticationResult, ValidationFailed>>;
