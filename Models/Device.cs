using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartIrrigatorAPI.Models;

public class Device
{
    [Key]
    public string SerialNumber { get; set; } = string.Empty;
    public byte[] Password { get; set; } =  Array.Empty<byte>();
    
    public byte[] Salt { get; set; } = Array.Empty<byte>();
    
    public Guid UserId { get; set; } = new Guid();
    
    public User? User { get; set; }
}