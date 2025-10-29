using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace ScheduleAndToDo.Views.CommonUI;

/// <summary>
/// Todolist.xaml の相互作用ロジック
/// </summary>
public partial class Todolist : UserControl
{
    public Todolist()
    {
        InitializeComponent();
    }
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource),typeof(IEnumerable),typeof(Todolist));
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register(nameof(DeleteCommand),typeof(ICommand),typeof(Todolist));
    public ICommand DeleteCommand
    {
        get => (ICommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }
    private ICommand _internalDeleteCommand;
    public ICommand InternalDeleteCommand 
    {
        get
        {
            if (_internalDeleteCommand==null)
            {
                _internalDeleteCommand = new RelayCommand<object>(item =>
                    {
                        DeleteCommand.Execute(item);
                    });
            }
            return _internalDeleteCommand;
        }
    }
}
