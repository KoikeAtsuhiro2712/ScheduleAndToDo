using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScheduleAndToDo.Views.CommonUI;

/// <summary>
/// BackButton.xaml の相互作用ロジック
/// </summary>
public partial class BackButton : UserControl
{
    public BackButton()
    {
        InitializeComponent();
    }
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command),typeof(ICommand),typeof(BackButton));
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty,value);
    }
}
