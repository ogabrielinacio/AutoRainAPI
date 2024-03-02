using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRainAPI.Models;

[Table("devices")]
public class Device
{
    [Key]
    [Column("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;
    
    [Column("password")]
    public string Password { get; set; } = string.Empty;
    
    [Column("salt")]
    public string Salt { get; set; } = string.Empty;
    
    [Column("user")]
    public User? User { get; set; }
}