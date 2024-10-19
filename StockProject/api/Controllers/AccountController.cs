using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto userData)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = userData.UserName,
                    Email = userData.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, userData.Password!);

                if (!createdUser.Succeeded) return StatusCode(500, createdUser.Errors);
                
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                
                if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);
                
                return Ok(new NewUserDto{
                    UserName = appUser.UserName!,
                    Email = appUser.Email!,
                    Token = _tokenService.CreateToken(appUser)
                });
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstAsync(
                user => user.UserName == userData.UserName
            );

            if (user == null) return Unauthorized("Invalid Credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                userData.Password!,
                false
            );

            if (!result.Succeeded) return Unauthorized("Invalid Credentials");

            return Ok(new NewUserDto{
                UserName = user.UserName!,
                Email = user.Email!,
                Token = _tokenService.CreateToken(user)
            });
        }

    }
}