namespace ScheduleAndToDo.Views.Message;

public class NavigationMessage
{
    public string Target { get; }
    public object? Parameter { get; }

    public NavigationMessage(string target,object? parameter=null)
    {
        Target = target;
        Parameter = parameter;
    }
}
