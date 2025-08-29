using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Contracts;
using AuthService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{ 
    private readonly IAuthenticationService _authenticationService;
    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequest request)
    {
        var response = await _authenticationService.Aunthenticate(request);

        if (response.IsFailure)
        {
            return Unauthorized(response.Error);
        }
        return Ok(response.Value);        
    }

    [Authorize]
    [HttpPost]
    [Route("refreshtoken")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest token)
    {
        var response = await _authenticationService.GetRefreshToken(token);

        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }
        return Ok(response.Value);
    }
        

    [AllowAnonymous]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout(RefreshTokenRequest token)
    {
        if(token is null || string.IsNullOrEmpty(token.Refresh_Token))
        {
            return BadRequest("Invalid client request");
        }

        await _authenticationService.Logout(token);

        return Ok();
    }
   


}
    
