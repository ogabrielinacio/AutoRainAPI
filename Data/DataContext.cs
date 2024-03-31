using Microsoft.EntityFrameworkCore;
using SmartIrrigatorAPI.Models;

namespace SmartIrrigatorAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
  
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>()
            .HasOne(u => u.User)
            .WithMany(d => d.Devices)
            .HasForeignKey(f => f.UserId);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceData> DeviceData { get; set; }
}