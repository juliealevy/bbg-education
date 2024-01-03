using BbgEducation.Application.Authentication.Common;
using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.UserDomain;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Authentication.Login;
public class LoginQueryHandler : IRequestHandler<LoginQuery, OneOf<AuthenticationResult, ValidationFailed>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<OneOf<AuthenticationResult, ValidationFailed>> Handle(LoginQuery query, CancellationToken cancellationToken) {
        await Task.CompletedTask;  //to get rid of warning until there is DB storage happening and repo call can be async

        User? user = _userRepository.GetUserByEmail(query.Email);

        if (user is null || (user.Password != query.Password)) {
            string propertyNames = $"{nameof(User.Email)} or {nameof(User.Password)}";
            return new ValidationFailed(propertyNames, "Email or Password is invalid.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
           token);
    }

}
