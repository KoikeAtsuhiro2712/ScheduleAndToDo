using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Views.Message;

namespace ScheduleAndToDo.ViewModels;

public partial class ScheduleWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string today=$"{DateTime.Now:yyyy年MM月dd日}の予定はこちら";
    [RelayCommand]
    private void OpenMain()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
}
