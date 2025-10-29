using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Views.Message;

namespace ScheduleAndToDo.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    //スケジュール画面に遷移する関数
    [RelayCommand]
    private void TransitionSchedule()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Schedule"));
    }
    //ToDo画面に遷移する関数
    [RelayCommand]
    private void TransitionToDo()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Todo"));
    }
}
