using AutoFixture;
using BbgEducation.Application.Authentication.Login;
using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.UserDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BbgEducation.Application.UnitTests.Authentication.Login;
public class LoginQueryHandlerTests
{
    private readonly LoginQueryHandler _testing;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IJwtTokenGenerator _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
    private readonly IFixture _fixture = new Fixture();

    public LoginQueryHandlerTests()
    {
        _testing = new LoginQueryHandler(_userRepository, _jwtTokenGenerator);
    }

    [Fact]
    public async void Handle_ShouldReturnSuccess_WhenInputIsValid() {

        var query = _fixture.Create<LoginQuery>();
        var user = User.Create("first", "last", query.Email, query.Password);
        var token = "new_jwt_token";
        _userRepository.GetUserByEmail(query.Email).Returns(user);
        _jwtTokenGenerator.GenerateToken(user).Returns(token);

        var result = await _testing.Handle(query, default);


        result.Should().NotBeNull();
        result.IsT0.Should().BeTrue();

        var resultValue = result.AsT0;
        resultValue.Should().NotBeNull();
        resultValue.Email.Should().Be(query.Email);
        resultValue.Token.Should().Be(token);
    }

    [Fact]
    public async void Handle_ShouldReturnFail_WhenEmailDoesNotExist() {

        var query = _fixture.Create<LoginQuery>();
        var user = User.Create("first", "last", query.Email, query.Password);       
        var errorMessage = "Email or Password is invalid.";

        _userRepository.GetUserByEmail(query.Email).ReturnsNull();

        var result = await _testing.Handle(query, default);

        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();

        var resultValue = result.AsT1;
        resultValue.Should().NotBeNull();
        resultValue.Errors.Should().NotBeNull().And.HaveCount(1);
        resultValue.Errors.FirstOrDefault()!.ErrorMessage.Should().Be(errorMessage);
    }

    [Fact]
    public async void Handle_ShouldReturnFail_WhenPasswordIsInvalid() {

        var query = _fixture.Create<LoginQuery>();
        var user = User.Create("first", "last", query.Email, "valid_password");        
        var errorMessage = "Email or Password is invalid.";

        _userRepository.GetUserByEmail(query.Email).Returns(user);

        var result = await _testing.Handle(query, default);

        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();

        var resultValue = result.AsT1;
        resultValue.Should().NotBeNull();
        resultValue.Errors.Should().NotBeNull().And.HaveCount(1);
        resultValue.Errors.FirstOrDefault()!.ErrorMessage.Should().Be(errorMessage);
    }
}
