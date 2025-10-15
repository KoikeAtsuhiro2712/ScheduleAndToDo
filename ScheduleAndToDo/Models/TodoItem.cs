using CommunityToolkit.Mvvm.ComponentModel;

namespace ScheduleAndToDo.Models;

public partial class TodoItem : ObservableObject
{
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private bool isCompleted;
}
