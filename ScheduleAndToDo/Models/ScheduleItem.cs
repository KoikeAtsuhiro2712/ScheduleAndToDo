using CommunityToolkit.Mvvm.ComponentModel;

namespace ScheduleAndToDo.Models;
public class ScheduleItem : ObservableObject
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Content { get; set; }

}
