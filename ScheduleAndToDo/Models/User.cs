namespace ScheduleAndToDo.Models;

public class AppUser
{
    public string AppUserId { get; set; }
    public string Password { get; set; }
    public string Hash { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}
