using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ScheduleAndToDo.Models;

public class TodoItemMessage : ValueChangedMessage<TodoItem>
{
    public TodoItemMessage(TodoItem item) : base(item) { }
}
