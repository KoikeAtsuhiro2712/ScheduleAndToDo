using ScheduleAndToDo.ViewModels;
using System.Windows;

namespace ScheduleAndToDo.Views;

/// <summary>
/// ScheduleWindow.xaml の相互作用ロジック
/// </summary>
public partial class ScheduleWindow : Window
{
    public ScheduleWindow()
    {
        InitializeComponent();
        DataContext = new ScheduleWindowViewModel();
    }
}
