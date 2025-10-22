using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace ScheduleAndToDo.Models;

public partial class TodoItem : ObservableObject
{
    [ObservableProperty]
    private string title = string.Empty;
    private bool isCompleted;

    public bool IsCompleted
    {
        get => isCompleted;
        set
        {
            if (SetProperty(ref isCompleted,value))
            {
                WeakReferenceMessenger.Default.Send(new TodoItemMessage(this));
            }
        }
    }
}
