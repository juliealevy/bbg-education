using BbgEducation.Application.Authentication.Register;
using FluentAssertions;

namespace BbgEducation.Application.UnitTests.Authentication.Register;
public  class RegisterCommandValidatorTests
{
    private readonly RegisterCommandValidator _testing = new RegisterCommandValidator();

    [Fact]
    public void Validate_Should_Pass_WhenValidInputIsProvided() {

        RegisterCommand command = new RegisterCommand("first", "last", "email@email.com", "12345");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("", "last", "email@email.com", "12345")]
    [InlineData("first", "", "email@email.com", "12345")]
    [InlineData("first", "last", "", "12345")]
    [InlineData("first", "last", "email@email.com", "")]
    [InlineData("", "", "email@email.com", "12345")]
    public void Validate_Should_Fail_WhenInputIsEmpty(string first, string last, string email, string password) {

        RegisterCommand command = new RegisterCommand(first,last, email,password);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }


    [Theory]
    [InlineData("emailAddress")]
    [InlineData("email.Address")]
    [InlineData("email@Address@com")]
    public void Validate_Should_Fail_WhenEmailIsInvalid(string email) {

        RegisterCommand command = new RegisterCommand("first", "last", email, "12345");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

}
