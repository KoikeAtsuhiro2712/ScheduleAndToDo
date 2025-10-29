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
    //今、表示しているスケジュールの前の月のスケジュールを表示する
    [RelayCommand]
    private void DisplayPriviousDay()
    {
        CurrentDate = CurrentDate.AddDays(-1);
        LoadSchedule();
    }
    //今、表示しているスケジュールの次の月のスケジュールを表示する
    [RelayCommand]
    private void DisplayNextDay()
    {
        CurrentDate = CurrentDate.AddDays(1);
        LoadSchedule();
    }
    //スケジュールメイン画面に遷移する
    [RelayCommand]
    private void BackSchedule()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Schedule"));
    }
    //schedule.jsonを読み取って、今後のスケジュールを表示する。
    private void LoadSchedule()
    {
        if (File.Exists(FilePath))
        { 
            //jsonファイルを読み込む
            var json=File.ReadAllText(FilePath);
            if (!string.IsNullOrEmpty(json))
            {
                var items = JsonSerializer.Deserialize<ObservableCollection<ScheduleItem>>(json);
                if (items!=null)
                {
                    //読み込んだデータから日付が該当の日付のもののみ、抜き出す。
                    var filter = items.Where(s => s.Date == currentDate);
                    SelectedDateScheduleItem.Clear();
                    //抜き出したデータを、表示する。
                    foreach (var item in filter)
                    {
                        selectedDateScheduleItem.Add(item);
                    }
                }
            }
        }
    }
}
