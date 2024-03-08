namespace SmartIrrigatorAPI.ViewModels;

public class DeviceHistoryViewModel
{
    public string SerialNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}