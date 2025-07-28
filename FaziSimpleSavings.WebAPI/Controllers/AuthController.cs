using Application.Features.Users.Commands.LoginUser;
using FaziSimpleSavings.Application.Common.Exceptions;
using FaziSimpleSavings.Application.Features.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(ApiResponse<string>.Ok(token, "User registered successfully"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(ApiResponse<string>.Ok(token, "User logged in successfully"));
    }

    [HttpGet("dashboard")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminDashboard()
    {
        return Ok(ApiResponse<object>.Ok(new { Message = "You are an admin." }, "Admin access confirmed"));
    }
}
