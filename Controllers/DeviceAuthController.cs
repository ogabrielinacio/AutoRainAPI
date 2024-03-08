using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartIrrigatorAPI.Data;
using SmartIrrigatorAPI.Models;
using SmartIrrigatorAPI.Utils;
using SmartIrrigatorAPI.ViewModels;

namespace SmartIrrigatorAPI.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("device")]
public class DeviceAuthController : ControllerBase
{
   private readonly DataContext _dataContext;

   public DeviceAuthController(DataContext dataContext)
   {
      _dataContext = dataContext;
   }

   [HttpPost("login")]
   public async Task<IActionResult> DeviceLogin([FromBody] DeviceLoginViewModel request)
   {
       Device? device = await _dataContext.Devices.FirstOrDefaultAsync(s => s.SerialNumber.Equals(request.SerialNumber));
       if(device == null)
           return Unauthorized("Device not Found");
       if (AuthenticationUtils.VerifyDevicePasswordHash(device, request.Password))
       {
           var userId = new Guid(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
           var user = await _dataContext.Users.Where(i => i.UserId.Equals(userId)).FirstOrDefaultAsync();
           if(user == null)
               return NotFound("User not found");
           if(device.UserId == userId)
               return BadRequest("User is already the administrator of device");
           device.UserId = userId;
           device.User = user;
           _dataContext.Devices.Update(device);
           await _dataContext.SaveChangesAsync();
           return Ok($"device Added with successfully ->{device.UserId} && {device.User.Name}");
       }
       return Unauthorized("Serial Number or password uncorrected");
   }
   
   [HttpPost("register")]
   public async Task<IActionResult> DeviceRegister([FromBody]  DeviceRegisterViewModel request) {

       if(_dataContext.Devices.Any(e => e.SerialNumber.Equals(request.SerialNumber)))
           return BadRequest("Serial Number already registered");
       AuthenticationUtils.CreatePasswordHash(request.Password, out byte[] hash, out byte[] salt);
       var device = new Device
       {
           SerialNumber = request.SerialNumber,
           Password = hash,
           Salt = salt,
       };
       await _dataContext.Devices.AddAsync(device);
       await _dataContext.SaveChangesAsync();

       return Ok("registered Device");
   }
}