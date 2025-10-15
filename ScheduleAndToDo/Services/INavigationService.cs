namespace ScheduleAndToDo.Services;

internal interface INavigationService
{
    void Navigate<TViewModel>(object parameter = null);
}