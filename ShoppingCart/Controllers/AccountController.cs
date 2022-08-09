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
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName && x.IsActive==true);

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
        #region SSO

        [HttpPost("ssoregister")]
        public async Task<ActionResult<UserDto>> SSORegister(RegisterDto registerDto)
        {
            try
            {
                if (registerDto != null)
                {
                    var _user = _context.Users.Where(x => x.Email == registerDto.Email).FirstOrDefault();
                    if (_user != null)
                    {
                        var userDto = new UserDto
                        {
                            UserId = _user.UserId,
                            UserName = _user.UserName,
                            FirstName = _user.FirstName,
                            LastName = _user.LastName,
                            Email = _user.Email,
                            Phone = _user.Phone,
                            IsActive = _user.IsActive,
                            token = _tokenService.CreateToken(_user)
                        };

                        return userDto;
                    }
                    else
                    {
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
                }

                else {
                    //  return BadRequest("Please insert corect data");
                    return BadRequest("you are not a valid user.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex}");
            }
           

           

           
        }

        [HttpPost("loginlog")]
        public async Task<ActionResult<UserDto>> LoginLog(LoginLogDto loginlog)
        {
            try
            {
                var _user = _context.Users.Where(x => x.UserId == loginlog.UserId).FirstOrDefault();
                var log = new LoginLog

                {
                    UserId = loginlog.UserId,
                    Provider = loginlog.Provider,
                    ProviderId = loginlog.ProviderId,

                    LoginTime = DateTime.Now,
                    LogoutTime = loginlog.LogoutTime,
                    CreatedOn = DateTime.Now,
                };

                _context.LoginLogs.Add(log);
                await _context.SaveChangesAsync();
                if (_user != null)
                {
                    var userDto = new UserDto
                    {
                        UserId = _user.UserId,
                        UserName = _user.UserName,
                        FirstName = _user.FirstName,
                        LastName = _user.LastName,
                        Email = _user.Email,
                        Phone = _user.Phone,
                        IsActive = _user.IsActive,
                        token = _tokenService.CreateToken(_user)
                    };

                    return userDto;
                }
                else
                {
                    return BadRequest("you are not a valid user.");
                }
                //  return Ok();

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex}");
            }

            

          
        }

        #endregion SSO
    }
}
