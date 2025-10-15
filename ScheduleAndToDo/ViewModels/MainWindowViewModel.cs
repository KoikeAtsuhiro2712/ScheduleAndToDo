using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Views.Message;

namespace ScheduleAndToDo.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [RelayCommand]
    private void OpenSchedule()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Schedule"));
    }
    [RelayCommand]
    private void OpenToDo()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Todo"));
    }
}
