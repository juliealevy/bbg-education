using AutoFixture;
using BbgEducation.Application.Authentication.Login;
using FluentAssertions;
using System.Net.Mail;

namespace BbgEducation.Application.UnitTests.Authentication.Login;
public  class LoginQueryValidatorTests
{
    private readonly LoginQueryValidator _testing = new LoginQueryValidator();
    private readonly IFixture _fixture = new Fixture();

    [Fact]
    public void Validate_Should_Pass_WhenValidInputIsProvided() {
        
        LoginQuery query = new LoginQuery(_fixture.Create<MailAddress>().ToString(), _fixture.Create<string>());

        var result = _testing.Validate(query);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);

    }

    [Theory]
    [InlineData("","12345")]
    [InlineData("email@email.com", "")]
    [InlineData("", "")]
    public void Validate_Should_Fail_WhenInputIsEmpty(string emailAddress, string password) {

        LoginQuery query = new LoginQuery(emailAddress, password);

        var result = _testing.Validate(query);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    [Theory]
    [InlineData("emailAddress")]
    [InlineData("email.Address")]
    [InlineData("email@Address@com")]
    public void Validate_Should_Fail_WhenEmailAddressIsInvalid(string emailAddress) {

        LoginQuery query = new LoginQuery(emailAddress, _fixture.Create<string>());

        var result = _testing.Validate(query);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }
}
