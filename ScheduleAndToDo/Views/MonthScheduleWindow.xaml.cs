using ScheduleAndToDo.ViewModels;
using System.Windows;

namespace ScheduleAndToDo.Views;

/// <summary>
/// MonthScheduleWindow.xaml の相互作用ロジック
/// </summary>
public partial class MonthScheduleWindow : Window
{
    public MonthScheduleWindow()
    {
        InitializeComponent();
        DataContext = new MonthScheduleWindowViewModel(); 
    }
}
