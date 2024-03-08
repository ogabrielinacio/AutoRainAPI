using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartIrrigatorAPI.Enums;

namespace SmartIrrigatorAPI.Models;
[Table("devices_data")]
public class DeviceData
{
    [Key]
    [Column("devices_data_id")]
    public Guid DevicesDataId { get; set; }

    [Required]
    [Column("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;

    [Column("status")]
    public EStatus Status { get; set; }
    
    [Column("soil_moisture")]
    public int SoilMoisture { get; set; }

    [Required]
    [Column("date")]
    public DateTime Date { get; set; }
}