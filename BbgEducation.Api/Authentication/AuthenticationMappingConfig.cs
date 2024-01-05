using BbgEducation.Application.Authentication.Login;
using BbgEducation.Application.Authentication.Register;
using Mapster;
using BbgEducation.Application.Authentication.Common;
using BbgEducation.Api.Hal;

namespace BbgEducation.Api.Authentication;

public class AuthenticationMappingConfig : IRegister
{
    //don't need to specifiy any properties that match
    public void Register(TypeAdapterConfig config) {
        //these are exactly the same, so technically don't need this, but here for any future changes and to document
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest._links, _ => new Dictionary<string, List<Link>>())
            .IgnoreNonMapped(true);
    }
}
