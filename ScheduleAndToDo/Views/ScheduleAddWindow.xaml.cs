using ScheduleAndToDo.ViewModels;
using System.Windows;

namespace ScheduleAndToDo.Views;

/// <summary>
/// ScheduleAdd.xaml の相互作用ロジック
/// </summary>
public partial class ScheduleAddWindow : Window
{
    public ScheduleAddWindow()
    {
        InitializeComponent();
        DataContext = new ScheduleAddWindowViewModel();
    }
}
