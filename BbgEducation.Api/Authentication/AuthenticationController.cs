using BbgEducation.Api.Api;
using BbgEducation.Api.Common;
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
public class AuthenticationController: ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IApiRouteService _apiRouteService;


    public AuthenticationController(ISender mediator, IMapper mapper, IApiRouteService apiRouteService) {
        _mediator = mediator;
        _mapper = mapper;
        _apiRouteService = apiRouteService;
    }


    [HttpPost(ApiRoutes.Authentication.Register)]
    public async Task<IActionResult> Register(RegisterRequest request) {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);
        return HandleAuthResult(authResult, true);
    }   

    [HttpPost(ApiRoutes.Authentication.Login)]
    public async Task<IActionResult> Login(LoginRequest request) {

        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        return HandleAuthResult(authResult, false);
    }

    private IActionResult HandleAuthResult(OneOf<AuthenticationResult, ValidationFailed> authResult, bool addLoginLink) {
        return authResult.Match<IActionResult>(
            auth =>
            {
                var authResponse = BuildAuthResponse(auth);
                if (addLoginLink) {
                    authResponse.AddLink("auth:login", _apiRouteService.GetRouteData(typeof(AuthenticationController), "Login")!, new LoginRequest("", ""));
                }
                return Ok(authResponse);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }

    private AuthenticationResponse BuildAuthResponse(AuthenticationResult result) {

        var response = _mapper.Map<AuthenticationResponse>(result);
        response.AddSelfLink(_apiRouteService.GetSelfRouteData(Request.RouteValues)!);

        return response;
    }
}
