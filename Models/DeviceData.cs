using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartIrrigatorAPI.Enums;

namespace SmartIrrigatorAPI.Models;
public class DeviceData
{
    [Key]
    public Guid DeviceDataId { get; set; }

    [Required]
    public string SerialNumber { get; set; } = string.Empty;

    public EStatus Status { get; set; }
    
    public int SoilMoisture { get; set; }

    [Required]
    public DateTime Date { get; set; }
}