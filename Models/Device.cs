using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartIrrigatorAPI.Models;

[Table("devices")]
public class Device
{
    [Key]
    [Column("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;

    [Column("password")] 
    public byte[] Password { get; set; } = new byte[0];
    
    [Column("salt")]
    public byte[] Salt { get; set; } = new byte[0];
    
    [Column("user_id")]
    [ForeignKey("FK_users_user_id")]
    
    public Guid? UserId { get; set; }
    
    [Column("user")]
    public User? User { get; set; }
}