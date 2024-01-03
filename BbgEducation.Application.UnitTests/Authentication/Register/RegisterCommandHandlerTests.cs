using AutoFixture;
using BbgEducation.Application.Authentication.Register;
using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.UserDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BbgEducation.Application.UnitTests.Authentication.Register;
public class RegisterCommandHandlerTests
{
    private readonly RegisterCommandHandler _testing;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IJwtTokenGenerator _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
    private readonly IFixture _fixture = new Fixture();

    public RegisterCommandHandlerTests()
    {
        _testing = new RegisterCommandHandler(_jwtTokenGenerator, _userRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnSuccess_WhenInputIsValid() {

        var cmd = _fixture.Create<RegisterCommand>();
        _userRepository.GetUserByEmail(cmd.Email).ReturnsNull();
        var id = Guid.NewGuid().ToString();   //not really used....not testing this here.
        var token = "new_jwt_token";
        //not testing token generation here, so just returning meaningless string.
        _jwtTokenGenerator.GenerateToken(id, cmd.FirstName, cmd.LastName, cmd.Email, cmd.Password)
            .Returns(token);

        var result = await _testing.Handle(cmd, default);


        result.Should().NotBeNull();
        result.IsT0.Should().BeTrue();

        var resultValue = result.AsT0;
        resultValue.Should().NotBeNull();       
        resultValue.Email.Should().Be(cmd.Email);
        resultValue.FirstName.Should().Be(cmd.FirstName);
        resultValue.LastName.Should().Be(cmd.LastName);
      
    }

    [Fact]
    public async void Handle_ShouldReturnFail_WhenEmailAlreadyRegistered() {

        var cmd = _fixture.Create<RegisterCommand>();
        _userRepository.GetUserByEmail(cmd.Email).Returns(User.Create("","","",""));

        var result = await _testing.Handle(cmd, default);


        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();

        var resultValue = result.AsT1;
        resultValue.Should().NotBeNull();
        resultValue.Errors.Should().NotBeNull();
        resultValue.Errors.Count().Should().Be(1);
        resultValue.Errors.FirstOrDefault()!.ErrorMessage.Should().Be("Email already exists");

    }
}
