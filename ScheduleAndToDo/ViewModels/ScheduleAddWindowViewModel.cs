using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Models;
using ScheduleAndToDo.Views.Message;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
namespace ScheduleAndToDo.ViewModels;

public partial class ScheduleAddWindowViewModel : ObservableObject
{
    private const string RegisterFilePath="schedule.json";

    [ObservableProperty]
    private DateTime date=DateTime.Now;

    [ObservableProperty]
    private TimeSpan startTime;

    [ObservableProperty]
    private TimeSpan endTime;

    [ObservableProperty]
    private string content;

    public ObservableCollection<ScheduleItem> Items { get; } = new();
    public ScheduleAddWindowViewModel()
    { 
        LoadData();
    }
    [RelayCommand]
    private void OpenSchedule()
    {
        SaveData();
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Schedule"));
    }
    [RelayCommand]
    private void Register()
    {
        var newItem = new ScheduleItem
        {
            Date= date,
            StartTime= startTime,
            EndTime= endTime,
            Content= content
        };
        Items.Add(newItem);
        SaveData();
        WeakReferenceMessenger.Default.Send(new NavigationMessage("ReloadSchedule"));
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Schedule"));

    }
    private void LoadData()
    {
        if (File.Exists(RegisterFilePath))
        {
            var json = File.ReadAllText(RegisterFilePath);
            if (!string.IsNullOrEmpty(json))
            {
                var items = JsonSerializer.Deserialize<ObservableCollection<ScheduleItem>>(json);
                if (items!= null)
                {
                    foreach (var item in items)
                        Items.Add(item);
                }
            }
        }
    }
    private void SaveData()
    {
        var json = JsonSerializer.Serialize(Items,new JsonSerializerOptions { WriteIndented=true});
        File.WriteAllText(RegisterFilePath, json);
    }
}
