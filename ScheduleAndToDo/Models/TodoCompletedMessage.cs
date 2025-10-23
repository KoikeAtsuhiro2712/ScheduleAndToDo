using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ScheduleAndToDo.Models;

class TodoCompletedMessage:ValueChangedMessage<TodoItem>
{
    public TodoCompletedMessage(TodoItem item) : base(item) { }
}
