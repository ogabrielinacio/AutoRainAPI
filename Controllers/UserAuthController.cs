using System.Security.Claims;
using AutoRainAPI.Data;
using AutoRainAPI.Models;
using AutoRainAPI.Utils;
using AutoRainAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoRainAPI.Controllers;

[ApiController]
public class UserAuthController: ControllerBase
{
    private readonly DataContext _dataContext;

    public UserAuthController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpPost("register")]

    public async Task<IActionResult> Register([FromBody] RegisterViewModel request) {

        if(_dataContext.Users.Any(e => e.Email.Equals(request.Email)))
            return BadRequest("Email already registered");
        AuthenticationUtils.CreatePasswordHash(request.Password, out byte[] hash, out byte[] salt);
        var user = new User{
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            Password = hash,
            Salt = salt,
        };
        user.Devices = new List<Device>();
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();

        return Ok("registered user");
    }

    [HttpPost("login")]

    public async Task<IActionResult> Login ([FromBody] LoginViewModel request ) {
        User user = await _dataContext.Users.Where(e => e.Email.Equals(request.Email)).FirstOrDefaultAsync();
        if(user == null)
            return Unauthorized("User not Found");
        if (AuthenticationUtils.VerifyPasswordHash(user, request.Password)) {
            return Ok(AuthenticationUtils.CreateToken(user));
        }
        return Unauthorized("email or password incorrected");
    }
}