using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.UserDomain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Authentication.Login;
public class LoginQueryHandler : IRequestHandler<LoginQuery, OneOf<AuthenticationResult, ValidationFailed>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator<LoginQuery> _validator;


    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IValidator<LoginQuery> validator) {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
    }

    public async Task<OneOf<AuthenticationResult, ValidationFailed>> Handle(LoginQuery query, CancellationToken cancellationToken) {
        await Task.CompletedTask;  //to get rid of warning until there is DB storage happening and repo call can be async

        var validate = _validator.Validate(query);

        if (!validate.IsValid) {
            return new ValidationFailed(validate.Errors);
        }

        User? user = _userRepository.GetUserByEmail(query.Email);

        if (user is null) {
            return CredentialsFailed();
        }

        if (user.Password != query.Password) {
            return CredentialsFailed();
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
           token);
    }

    /// <summary>
    /// Builds generic credentials validation failed to hide if it was the email or password that was wrong for security"
    /// </summary>
    /// <returns></returns>
    private static OneOf<AuthenticationResult, ValidationFailed> CredentialsFailed() {
        string propertyNames = $"{nameof(User.Email)} or {nameof(User.Password)}";
        return new ValidationFailed(new ValidationFailure(propertyNames, "Email or Password is invalid."));
    }
}
