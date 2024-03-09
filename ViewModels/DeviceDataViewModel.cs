using SmartIrrigatorAPI.Enums;

namespace SmartIrrigatorAPI.ViewModels;

public class DeviceDataViewModel
{
    public string SerialNumber { get; set; } = string.Empty;

    public EStatus Status { get; set; }
    
    public int SoilMoisture { get; set; }

    public DateTime Date { get; set; } 
}