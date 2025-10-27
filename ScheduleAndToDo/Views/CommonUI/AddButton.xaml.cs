using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScheduleAndToDo.Views.CommonUI
{
    /// <summary>
    /// AddButton.xaml の相互作用ロジック
    /// </summary>
    public partial class AddButton : UserControl
    {
        public AddButton()
        {
            InitializeComponent();
        }
    public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(AddButton));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
