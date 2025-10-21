using System.Collections.ObjectModel;

namespace ScheduleAndToDo.Models;

public class ScheduleGroup
{
    public DateOnly date { get; set; }
    public ObservableCollection<ScheduleItem> items { get; set; }
}
