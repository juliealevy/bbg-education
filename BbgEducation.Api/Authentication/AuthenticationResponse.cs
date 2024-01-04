using BbgEducation.Api.Links;

namespace BbgEducation.Api.Authentication;

public class AuthenticationResponse : HalResponse
{
    public AuthenticationResponse() {}
    public AuthenticationResponse(string id, string firstName, string lastName, string email, string token): base() {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Token = token;
    }

    public string Id { get; init; } = "";
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";
    public string Email { get; init; } = "";
    public string Token { get; init; } = "";
}   
