using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ScheduleAndToDo.Models;

public class AppDbContext : DbContext
{
    public DbSet<AppUser> User { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=test.db");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().ToTable("AppUser");
    }
}
