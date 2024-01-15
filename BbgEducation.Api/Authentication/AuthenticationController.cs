using BbgEducation.Api.Common.Authentication;
using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Api.Hal;
using BbgEducation.Application.Authentication.Common;
using BbgEducation.Application.Authentication.Login;
using BbgEducation.Application.Authentication.Register;
using BbgEducation.Application.Common.Validation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace BbgEducation.Api.Authentication;

[AllowAnonymous]
[Route("auth")]
public class AuthenticationController: ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IBbgLinkGenerator _linkGenerator;
    private readonly IRepresentationFactory _representationFactory;

    public AuthenticationController(ISender mediator, IMapper mapper, IBbgLinkGenerator linkGenerator, IRepresentationFactory representationFactory) {
        _mediator = mediator;
        _mapper = mapper;
        _linkGenerator = linkGenerator;
        _representationFactory = representationFactory;
    }


    [HttpPost("register")]
    [Consumes("application/json")]
    [Produces("application/hal+json")]
    public async Task<IActionResult> Register(RegisterRequest request) {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);
        return HandleAuthResult(authResult, true);
    }

    //just for testing for now
    [HttpDelete("clear-all")]
    public async Task<IActionResult> ClearAllUsers() {
        var command = new ClearCommand();
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPost("login")]
    [Consumes("application/json")]
    [Produces("application/hal+json")]
    public async Task<IActionResult> Login(LoginRequest request) {

        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        return HandleAuthResult(authResult, false);
    }

    [HttpPost("logout/{username}")]
    [Consumes("application/json")]
    [Produces("application/hal+json")]
    public IActionResult Logout(string username) {

        return Ok($"{username} is logged out");
    }

    private IActionResult HandleAuthResult(OneOf<AuthenticationResult, ValidationFailed> authResult, bool addLoginLink) {
        return authResult.Match<IActionResult>(
            auth =>
            {
                var representation = _representationFactory.NewRepresentation(HttpContext)
                    .WithObject(auth);

                if (addLoginLink) {
                    representation
                        .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Authentication.LOGIN, typeof(AuthenticationController), "Login", null)!)
                        .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Authentication.LOGOUT, typeof(AuthenticationController), "Logout",
                            new { username = "JulieLevy" })!);  //just a test,no logout logic
                }
                return Ok(representation);
            },
            failed => BuildActionResult(failed)
            ) ;
    }   
}
