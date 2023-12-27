using BbgEducation.Application.Common.Exceptions;
using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.UserDomain;
using MediatR;

namespace BbgEducation.Application.Authentication.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken) {
        await Task.CompletedTask;  //to get rid of warning until there is asynchronous logic

        //check if user already exists
        if (_userRepository.GetUserByEmail(command.Email) is not null) {
            throw new DuplicateEmailException();
        }

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password);

        _userRepository.Add(user);

        //create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);

    }
}
