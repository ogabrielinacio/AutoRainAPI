using AutoRainAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRainAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
  
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey("SerialNumber");
                entity.HasOne(r => r.User)
                    .WithMany(r => r.Devices)
                    .HasForeignKey(r => r.FKUserId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            }
        );
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceData> DeviceData { get; set; }
}