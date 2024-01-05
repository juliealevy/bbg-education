using BbgEducation.Api.Api;
using BbgEducation.Api.Common;
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

    public AuthenticationController(ISender mediator, IMapper mapper, IBbgLinkGenerator linkGenerator) {        
        _mediator = mediator;
        _mapper = mapper;
        _linkGenerator = linkGenerator;
    }

    
    [HttpPost("register")]
    [Consumes("application/json")]
    [Produces("application/hal+json")]
    public async Task<IActionResult> Register(RegisterRequest request) {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);
        return HandleAuthResult(authResult, true);
    }

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
    public IActionResult Logout(string username) {

        return Ok($"{username} is logged out");
    }

    private IActionResult HandleAuthResult(OneOf<AuthenticationResult, ValidationFailed> authResult, bool addLoginLink) {
        return authResult.Match<IActionResult>(
            auth =>
            {
                var authResponse = BuildAuthResponse(auth);
                if (addLoginLink) {
                    authResponse.AddLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Authentication.LOGIN, typeof(AuthenticationController), "Login", null));
                    authResponse.AddLink(_linkGenerator.GetActionLink(HttpContext, "auth:logout", typeof(AuthenticationController), "Logout", new {username="Julie" }));
                }
                return Ok(authResponse);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }

    private AuthenticationResponse BuildAuthResponse(AuthenticationResult result) {

        var response = _mapper.Map<AuthenticationResponse>(result);
        response.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));

        return response;
    }
}
