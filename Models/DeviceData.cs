using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoRainAPI.Enums;

namespace AutoRainAPI.Models;
[Table("devices_data")]
public class DeviceData
{
    [Key]
    [Column("devices_data_id")]
    public Guid devices_data_id { get; set; }

    [Required]
    [Column("serial_number")]
    public string serial_number { get; set; } = string.Empty;

    [Column("status")]
    public EStatus Status { get; set; }
    
    [Column("soil_moisture")]
    public int SoilMoisture { get; set; }

    [Required]
    [Column("date")]
    public DateTime date { get; set; }
}