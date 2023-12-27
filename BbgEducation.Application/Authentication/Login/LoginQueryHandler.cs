using BbgEducation.Application.Common.Exceptions;
using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.UserDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Authentication.Login;
public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken) {
        await Task.CompletedTask;  //to get rid of warning until there is asynchronous logic

        User? user = _userRepository.GetUserByEmail(query.Email);

        if (user is null) {
            throw new InvalidCredentialsException();
        }

        if (user.Password != query.Password) {
            throw new InvalidCredentialsException();
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
