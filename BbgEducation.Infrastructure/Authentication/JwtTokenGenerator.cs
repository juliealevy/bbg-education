using BbgEducation.Application.Common.Interfaces.Authentication;
using BbgEducation.Application.Common.Interfaces.Services;
using BbgEducation.Domain.UserDomain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BbgEducation.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions) {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }



    public string GenerateToken(User user) {

        return GenerateToken(user.Id, user.FirstName, user.LastName, user.Email, user.Password);
    }

    public string GenerateToken(string id, string firstName, string lastName, string email, string password) {
        //symmetric key because we are issuing and validating the token ourselves
        //if using an identity server, need to change
        var signingCreds = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, id),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCreds);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
