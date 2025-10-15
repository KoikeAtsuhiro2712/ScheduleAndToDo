using CommunityToolkit.Mvvm.ComponentModel;

namespace ScheduleAndToDo.ViewModels;

internal class BaseViewModel:ObservableRecipient
{
    public virtual void OnNavigatedTo(object parameter) { }
}
