using Application.Features.Authentication.Command.LoginCommand;
using Application.Features.Authentication.Command.RegisterCommand;
using Core.Security.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : BaseController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        var result = await Mediator.Send(registerCommand);
        return Created("",result);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        var result = await Mediator.Send(loginCommand);
        return Ok(result);
    }
    
    [HttpPost("refresh-token-login")]
    public async Task<IActionResult> Login([FromBody] RefreshTokenLoginCommand refreshTokenLoginCommand)
    {
        var result = await Mediator.Send(refreshTokenLoginCommand);
        return Ok(result);
    }
}