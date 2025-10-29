using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScheduleAndToDo.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Views.Message;

namespace ScheduleAndToDo.ViewModels;

public partial class MonthScheduleWindowViewModel : ObservableObject
{
    private const string FilePath = "schedule.json";
    private int currentMonthDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
    [ObservableProperty]
    private DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
    public ObservableCollection<ScheduleItem> selectedMonthScheduleItem;
    [ObservableProperty]
    private ObservableCollection<ScheduleGroup> groupedMonthScheduleItem=new();
    public MonthScheduleWindowViewModel()
    {
        Generate();
    }
    //今、表示されている月の前の月のスケジュールを表示する。
    [RelayCommand]
    private void PreviousMonth()
    {
        CurrentDate = CurrentDate.AddMonths(-1);
        Generate();
    }
    //今、表示されている月の次の月のスケジュールを表示する。
    [RelayCommand]
    private void NextMonth()
    {
        CurrentDate = CurrentDate.AddMonths(1);
        Generate();
    }
    //スケジュール画面に遷移する
    [RelayCommand]
    private void BackSchedule()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Schedule"));
    }
    private void Generate()
    {
        ObservableCollection<ScheduleItem> items = new();
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            if (!string.IsNullOrEmpty(json))
            {
                items = JsonSerializer.Deserialize<ObservableCollection<ScheduleItem>>(json);
                if (items!=null)
                {
                    selectedMonthScheduleItem = items;               
                }
            }
        }
        groupedMonthScheduleItem.Clear();
        currentMonthDays = DateTime.DaysInMonth(currentDate.Year,currentDate.Month);
        for (int day=1;day<=currentMonthDays;day++)
        {
            var date = new DateOnly(currentDate.Year,currentDate.Month,day);
            var dayItems = items
                .Where(s =>s.Date==date)
                .OrderBy(s=>s.StartTime);

            GroupedMonthScheduleItem.Add(new ScheduleGroup
            {
                date = date,
                items = new ObservableCollection<ScheduleItem>(dayItems)
            });

        }
       
    }
}
