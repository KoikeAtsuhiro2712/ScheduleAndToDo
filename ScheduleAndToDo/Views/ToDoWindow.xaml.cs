using ScheduleAndToDo.ViewModels;
using System.Windows;

namespace ScheduleAndToDo.Views;

/// <summary>
/// ToDoWindow.xaml の相互作用ロジック
/// </summary>
public partial class ToDoWindow : Window
{
    public ToDoWindow()
    {
        InitializeComponent();
        DataContext = new ToDoWindowViewModel();
    }
}
