using AutoRainAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRainAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceData> DeviceData { get; set; }
}