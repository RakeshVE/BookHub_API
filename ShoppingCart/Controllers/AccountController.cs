using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ShoppingCartContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(ShoppingCartContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest("Username is already taken");

            using var hmac = new HMACSHA512();

            var user = new User

            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                IsActive = true,
                CreatedOn = DateTime.Now,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive,
                token = _tokenService.CreateToken(user)
            };

            return userDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive,
                token = _tokenService.CreateToken(user)
            };

            return userDto;
        }

        [HttpGet]
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpGet("usernamecheck")]
        public async Task<ActionResult> usernamecheck(string username)
        {
            try
            {
                if (await UserExists(username)) return BadRequest("Username is already taken");
                else return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex}");
            }
           
        }
        [HttpPost("forgotpassword")]
        public async Task<ActionResult> ForgotPassword(RegisterDto registerDto)
        {
            try
            {
                User _user = new User();
                _user = _context.Users.Where(x => x.UserName == registerDto.UserName.ToLower() && x.Email == registerDto.Email.ToLower()).FirstOrDefault();
                if (_user != null)
                {
                    using var hmac = new HMACSHA512();

                    _user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
                    _user.PasswordSalt = hmac.Key;
                    _user.ModifiedOn = DateTime.Now;

                    _context.Users.Update(_user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Please insert corect data");
                }


                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex}");
            }
           
        }

    }
}
