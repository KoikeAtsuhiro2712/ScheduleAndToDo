using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Models;
using ScheduleAndToDo.Views.Message;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace ScheduleAndToDo.ViewModels;

public partial class DayScheduleWindowViewModel : ObservableObject
{
    private const string FilePath = "schedule.json";
    [ObservableProperty]
    private DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
    [ObservableProperty]
    private ObservableCollection<ScheduleItem> selectedDateScheduleItem = new();
    public DayScheduleWindowViewModel()
    {
        LoadSchedule();
    }
    [RelayCommand]
    private void PriviousDay()
    {
        CurrentDate = CurrentDate.AddDays(-1);
        LoadSchedule();
    }
    [RelayCommand]
    private void NextDay()
    {
        CurrentDate = CurrentDate.AddDays(1);
        LoadSchedule();
    }
    [RelayCommand]
    private void BackSchedule()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Schedule"));
    }
    private void LoadSchedule()
    {
        if (File.Exists(FilePath))
        { 
            var json=File.ReadAllText(FilePath);
            if (!string.IsNullOrEmpty(json))
            {
                var items = JsonSerializer.Deserialize<ObservableCollection<ScheduleItem>>(json);
                if (items!=null)
                {
                    var filter = items.Where(s => s.Date == currentDate);
                    SelectedDateScheduleItem.Clear();
                    foreach (var item in filter)
                    {
                        selectedDateScheduleItem.Add(item);
                    }
                }
            }
        }
    }
}
