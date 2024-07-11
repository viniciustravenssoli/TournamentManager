using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TournamentAPI.DTOs;
using TournamentAPI.Models;
using TournamentAPI.Services.User;

namespace TournamentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

         [HttpPost("Create")]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDTO request)
        {
            await _userService.Create(request);

            return Created();
            
        }

         [HttpPost("Login")]
        public async Task<ActionResult<ResponseLogin>> Login([FromBody] LoginRequest request)
        {
            var result = await _userService.Login(request.Password, request.Email);

            return Created(string.Empty, result);
            
        }
    }
}