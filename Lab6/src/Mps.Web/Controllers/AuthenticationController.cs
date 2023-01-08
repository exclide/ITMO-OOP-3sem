using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.LoginCQ;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] EmployeeLoginModel model)
    {
        var command = new LoginEmployeeCommand(model.Login, model.Password);
        var response = await _mediator.Send(command, CancellationToken);

        if (response is null)
        {
            return AccessDenied();
        }

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Name, response.FullName.FirstName + response.FullName.LastName),
            new Claim(ClaimTypes.Sid, response.Id.ToString()),
            new Claim(ClaimTypes.Role, response.EmployeeRole.ToString("G")),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties() { IsPersistent = true});

        return Ok(response);
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
    
    [Route("error")]
    [HttpGet]
    [HttpPost]
    public IActionResult AccessDenied()
    {
        Claim? roleClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
        return roleClaim is not null
            ? StatusCode((int)HttpStatusCode.Forbidden, $"User with role {roleClaim.Value} is not authorized to invoke this method")
            : Unauthorized("User is not authenticated");
    }
}