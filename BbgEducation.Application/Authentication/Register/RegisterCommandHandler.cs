using BbgEducation.Application.Authentication.Common;
using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.UserDomain;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Authentication.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OneOf<AuthenticationResult, ValidationFailed>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;    

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<OneOf<AuthenticationResult, ValidationFailed>> Handle(RegisterCommand command, CancellationToken cancellationToken) {
        
        await Task.CompletedTask;  //to get rid of warning until there is asynchronous logic                     s
        
        if (_userRepository.GetUserByEmail(command.Email) is not null) {
            return ValidationFailed.Conflict(nameof(RegisterCommand.Email), "Email already exists");
        }

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password);

        _userRepository.Add(user);

        //Question:  should register generate a token?  Or require user to log in after registration?
        var token = _jwtTokenGenerator.GenerateToken(user);

        var authResult =  new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);

        return authResult;

    }
}
