using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using ShoppingCart.Repositories;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggerManager _loggerManager;

        public UsersController(IUserRepository userRepository, ILoggerManager loggerManager)
        {
            _userRepository = userRepository;
            _loggerManager = loggerManager;
    }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user;
        }

        [HttpGet("DeactivateUser")]
        public async Task<IActionResult> DeactivateUser(int userId)
        {
            try
            {
                if(userId < 0)
                {
                    return BadRequest("Invalid Id");
                }
                var user = await _userRepository.DeactivtaeUser(userId);
                return Ok(user);
            }
            catch(Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
