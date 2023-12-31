using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.UserDomain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Authentication.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OneOf<AuthenticationResult, ValidationFailed>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterCommand> _validator;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IValidator<RegisterCommand> validator) {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _validator = validator;
    }
    public async Task<OneOf<AuthenticationResult, ValidationFailed>> Handle(RegisterCommand command, CancellationToken cancellationToken) {
        await Task.CompletedTask;  //to get rid of warning until there is asynchronous logic
                
        var validate = _validator.Validate(command);

        if (!validate.IsValid) {
            return new ValidationFailed(validate.Errors);
        }

        if (_userRepository.GetUserByEmail(command.Email) is not null) {
            return new ValidationFailed(new ValidationFailure(nameof(RegisterCommand.Email), "Email already exists"));
        }

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password);

        _userRepository.Add(user);

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
