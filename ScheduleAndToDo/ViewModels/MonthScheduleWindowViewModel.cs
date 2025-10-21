using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScheduleAndToDo.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ScheduleAndToDo.ViewModels;

public partial class MonthScheduleWindowViewModel : ObservableObject
{
    private const string FilePath = "schedule.json";
    private int currentMonthDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
    [ObservableProperty]
    public string yearMonth = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy年M月");
    [ObservableProperty]
    private DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
    public ObservableCollection<ScheduleItem> selectedMonthScheduleItem;
    [ObservableProperty]
    private ObservableCollection<ScheduleGroup> groupedMonthScheduleItem=new();
    public MonthScheduleWindowViewModel()
    {
        Generate();
    }
    [RelayCommand]
    private void PreviousMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        Generate();
    }
    [RelayCommand]
    private void NextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        Generate();
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
        currentMonthDays = DateTime.DaysInMonth(CurrentDate.Year,currentDate.Month);
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
