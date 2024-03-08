using System.Security.Claims;
using AutoRainAPI.Data;
using AutoRainAPI.Enums;
using AutoRainAPI.Models;
using AutoRainAPI.Utils;
using AutoRainAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace AutoRainAPI.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("device")]
public class DeviceController : ControllerBase
{
   private readonly DataContext _dataContext;

   public DeviceController(DataContext dataContext)
   {
      _dataContext = dataContext;
   }

   [HttpPost("mock-data")]
   public async Task<IActionResult> Mock([FromBody] DeviceLatestDataViewModel request)
   {
       Device? device =
           await _dataContext.Devices.FirstOrDefaultAsync(s => s.SerialNumber.Equals(request.SerialNumber));
       if(device == null)
           return BadRequest("Device not Found");
       DeviceData latestData = new DeviceData
       {
           SerialNumber = device.SerialNumber,
           Status = EStatus.Online,
            SoilMoisture = new Random().Next(1, 100),
            Date = DateTime.UtcNow,
       };
       _dataContext.DeviceData.Update(latestData);
       await _dataContext.SaveChangesAsync();
       return Ok(latestData);
   }
   
   [HttpPost("latest-data")]
   public async Task<IActionResult> DeviceLatestData([FromBody] DeviceLatestDataViewModel request)
   {
       Device? device =
           await _dataContext.Devices.FirstOrDefaultAsync(s => s.SerialNumber.Equals(request.SerialNumber));
       if(device == null)
           return BadRequest("Device not Found");
       DeviceData? latestData = await _dataContext.DeviceData.Where(s => s.SerialNumber.Equals(request.SerialNumber)).OrderBy(d => d.Date).LastOrDefaultAsync();
       if (latestData == null)
           return NoContent();
       return Ok(latestData);
   }
}