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
    //スケジュール追加画面に遷移する
    [RelayCommand]
    private void OpenScheduleAdd()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("ScheduleAdd"));
    }
    //押すと、中のコンテキストメニューが開くようになっている
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
    //メイン画面に戻るようにしている
    [RelayCommand]
    private void OpenMain()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
    //コンテキストメニューの中の日毎スケジュール画面に遷移
    [RelayCommand]
    private void OpenDaySchedule()
    {
        //MessageBox.Show("DayScheduleに遷移します","画面遷移",MessageBoxButton.OKCancel,MessageBoxImage.Information);
        WeakReferenceMessenger.Default.Send(new NavigationMessage("DaySchedule"));
    }
    //コンテキストメニューの中の月毎スケジュール画面に遷移
    [RelayCommand]
    private void OpenMonthSchedule()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("MonthSchedule"));
    }
}
