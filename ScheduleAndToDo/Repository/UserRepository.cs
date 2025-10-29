using Microsoft.EntityFrameworkCore;
using ScheduleAndToDo.Models;

namespace ScheduleAndToDo.Repository;

public class UserRepository
{
    public async Task AddUserAsync(AppUser user)
    {
        using var db = new AppDbContext();
        db.User.Add(user);
        await db.SaveChangesAsync();
    }
    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        using var db = new AppDbContext();
        return await db.User.ToListAsync();
    }
    public async Task<AppUser?> GetUserByIdAsync(int id)
    { 
        using var db=new AppDbContext();
        return await db.User.FindAsync(id); 
    }
    public async Task<AppUser?> GetUserByPasswordAsync(string password)
    {
        using var db = new AppDbContext();
        return await db.User.FirstOrDefaultAsync(u=> u.Password==password);
    }
}
