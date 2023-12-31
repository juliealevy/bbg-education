using BbgEducation.Api.Api;
using BbgEducation.Api.Common;
using BbgEducation.Application.Authentication;
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


    public AuthenticationController(ISender mediator, IMapper mapper) {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost(ApiRoutes.Authentication.Register)]
    public async Task<IActionResult> Register(RegisterRequest request) {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);
        return HandleAuthResult(authResult);
    }   

    [HttpPost(ApiRoutes.Authentication.Login)]
    public async Task<IActionResult> Login(LoginRequest request) {

        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        return HandleAuthResult(authResult);
    }

    private IActionResult HandleAuthResult(OneOf<AuthenticationResult, ValidationFailed> authResult) {
        return authResult.Match<IActionResult>(
            auth => Ok(_mapper.Map<AuthenticationResponse>(authResult.Value)),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }
}
