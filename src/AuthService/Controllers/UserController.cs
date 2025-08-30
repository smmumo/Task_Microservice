using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Contracts;
using AuthService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]       
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
        {
            var response = await _userService.CreateUser(request);

            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return StatusCode(201);
        }
    }
}