using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Models;
using ScheduleAndToDo.Views.Message;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
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
                    selectedDateScheduleItem = new ObservableCollection<ScheduleItem>(items.OrderBy(s => s.Date));
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
    private void OpenContextMenu(Button? button)
    {
        if (button?.ContextMenu!=null)
        {
            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            button.ContextMenu.IsOpen = true;
        }
    }
    [RelayCommand]
    private void OpenMain()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
    [RelayCommand]
    private void OpenDaySchedule()
    {
        //MessageBox.Show("DayScheduleに遷移します","画面遷移",MessageBoxButton.OKCancel,MessageBoxImage.Information);
        WeakReferenceMessenger.Default.Send(new NavigationMessage("DaySchedule"));
    }
    [RelayCommand]
    private void OpenMonthSchedule()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("MonthSchedule"));
    }
}
