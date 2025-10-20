using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Views.Message;
using System.Collections.ObjectModel;
using ScheduleAndToDo.Models;
using System.IO;
using System.Text.Json;
namespace ScheduleAndToDo.ViewModels;

public partial class ScheduleWindowViewModel : ObservableObject
{
    private const string SaveFilePath = "schedule.json";
    [ObservableProperty]
    public ObservableCollection<ScheduleItem> selectedDateScheduleItem = new();
    public ScheduleWindowViewModel()
    {
        LoadSchedule();
    }
    public void LoadSchedule()
    {
        if (File.Exists(SaveFilePath))
        {
            var json = File.ReadAllText(SaveFilePath);
            if (!string.IsNullOrEmpty(json))
            {
                var items = JsonSerializer.Deserialize<ObservableCollection<ScheduleItem>>(json);
                if (items != null)
                {
                    selectedDateScheduleItem = items;
                }
            }
        }
    }
    [RelayCommand]
    private void OpenScheduleAdd()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("ScheduleAdd"));
    }
    [RelayCommand]
    private void OpenMain()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
}
