using BbgEducation.Api.Common;
using BbgEducation.Application.Authentication;
using BbgEducation.Application.Authentication.Login;
using BbgEducation.Application.Authentication.Register;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.Authentication;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController: ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;


    public AuthenticationController(ISender mediator, IMapper mapper) {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request) {
        var command = _mapper.Map<RegisterCommand>(request);
        AuthenticationResult registerResult = await _mediator.Send(command);
        return Ok(_mapper.Map<AuthenticationResponse>(registerResult));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request) {

        var query = _mapper.Map<LoginQuery>(request);
        AuthenticationResult loginResult = await _mediator.Send(query);


        return Ok(_mapper.Map<AuthenticationResponse>(loginResult));
    }
}
