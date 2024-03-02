using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRainAPI.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("last_name")]
    public string LastName { get; set; } = string.Empty;
    
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    
    [Column("password")]
    public byte[] Password { get; set; } = new byte[0];
    
    [Column("salt")]
    public byte[] Salt { get; set; } = new byte[0];

    [Column("devices")]
    public List<Device>? Devices { get; set; }
}