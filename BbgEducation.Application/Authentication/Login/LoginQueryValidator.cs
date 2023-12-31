using FluentValidation;

namespace BbgEducation.Application.Authentication.Login;
public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator() {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();  //could also add allowed characters??
    }
}
