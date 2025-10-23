using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace ScheduleAndToDo.Models;

public partial class TodoItem : ObservableObject
{
    [ObservableProperty]
    private string content = string.Empty;
    [ObservableProperty]
    private DateTime deadlinedate=DateTime.Now;
    private bool isCompleted;
    public bool IsCompleted
    {
        get => isCompleted;
        set
        {
            if (SetProperty(ref isCompleted,value))
            {
                WeakReferenceMessenger.Default.Send(new TodoCompletedMessage(this));
            }
        }
    }
}
