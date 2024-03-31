using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartIrrigatorAPI.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public byte[] Password { get; set; } = Array.Empty<byte>();
    
    public byte[] Salt { get; set; } = Array.Empty<byte>();

    public List<Device>? Devices { get; set; } = new List<Device>();
}