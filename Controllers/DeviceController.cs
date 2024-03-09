using System.Security.Claims;
using SmartIrrigatorAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartIrrigatorAPI.Data;
using SmartIrrigatorAPI.Enums;
using SmartIrrigatorAPI.Models;
using SmartIrrigatorAPI.ViewModels;


namespace SmartIrrigatorAPI.Controllers;

[ApiController]
[Authorize(Roles = "User")]
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
       
       Guid userId = new Guid(User.Claims.First(
           c => c.Type == ClaimTypes.NameIdentifier).Value);

       Device? userPermission = await _dataContext.Devices.FirstOrDefaultAsync(d => 
           d.UserId.Equals(userId));
       if(userPermission == null) 
           return Unauthorized("You don't have permission to make requests on this device");
       
       DeviceData? deviceData = await _dataContext.DeviceData.Where(
           s => s.SerialNumber.Equals(request.SerialNumber))
           .OrderBy(d => d.Date).LastOrDefaultAsync();
       if (deviceData == null)
           return NoContent();
       var latestData = new DeviceDataViewModel {
           SerialNumber = deviceData.SerialNumber,
           Status = deviceData.Status,
           SoilMoisture = deviceData.SoilMoisture,
           Date = deviceData.Date
       };
       return Ok(latestData);
   }

   [HttpPost("history")]
   public async Task<ActionResult> DeviceHistory([FromBody] DeviceHistoryViewModel request)
   {
       Device? device =
           await _dataContext.Devices.FirstOrDefaultAsync(s => s.SerialNumber.Equals(request.SerialNumber));
       if(device == null)
           return BadRequest("Device not Found");
       
       Guid userId = new Guid(User.Claims.First(
           c => c.Type == ClaimTypes.NameIdentifier).Value);

       Device? userPermission = await _dataContext.Devices.FirstOrDefaultAsync(d => 
           d.UserId.Equals(userId));
       if(userPermission == null) 
           return Unauthorized("You don't have permission to make requests on this device");
       List<DeviceData>? historyData = await _dataContext.DeviceData.Where(
               d => d.SerialNumber.Equals(request.SerialNumber) 
                    && DateTime.Compare(d.Date, request.StartDate) >= 0 
                    && DateTime.Compare(d.Date, request.EndDate) <= 0 )
           .OrderBy(d => d.Date).ToListAsync();
       List<DeviceDataViewModel> historyDataViewModel = new List<DeviceDataViewModel>();
       historyData.ForEach(v => {
           var deviceData = new DeviceDataViewModel {
               SerialNumber = v.SerialNumber,
               Status = v.Status,
               SoilMoisture = v.SoilMoisture,
               Date = v.Date
           };
           historyDataViewModel.Add(deviceData);
       });
       return Ok(historyDataViewModel);
   }
}