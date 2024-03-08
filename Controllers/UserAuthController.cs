using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartIrrigatorAPI.Data;
using SmartIrrigatorAPI.Models;
using SmartIrrigatorAPI.Utils;
using SmartIrrigatorAPI.ViewModels;

namespace SmartIrrigatorAPI.Controllers;

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

    public async Task<IActionResult> Login ([FromBody] LoginViewModel request )
    {
        User? user = await _dataContext.Users.FirstOrDefaultAsync(e => e.Email.Equals(request.Email));
        if(user == null)
            return Unauthorized("User not Found");
        if (AuthenticationUtils.VerifyPasswordHash(user, request.Password)) {
            return Ok(AuthenticationUtils.CreateToken(user));
        }
        return Unauthorized("email or password uncorrected");
    }
}