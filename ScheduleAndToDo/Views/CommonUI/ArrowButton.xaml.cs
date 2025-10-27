using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScheduleAndToDo.Views.CommonUI;

/// <summary>
/// ArrowButton.xaml の相互作用ロジック
/// </summary>
public partial class ArrowButton : UserControl
{
    public ArrowButton()
    {
        InitializeComponent();
    }
    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register(nameof(ButtonText),typeof(string),typeof(ArrowButton),new PropertyMetadata(string.Empty));
    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ArrowButton));
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty,value);
    }
}
