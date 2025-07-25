using Application.Features.Users.Commands.LoginUser;
using FaziSimpleSavings.Application.Features.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaziSimpleSavings.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }
}
